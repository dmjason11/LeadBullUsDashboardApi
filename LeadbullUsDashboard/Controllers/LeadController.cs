using Api.DTOS;
using Api.Errors;
using AutoMapper;
using Core;
using Core.IRepos;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace Api.Controllers
{
    [Authorize]
    public class LeadController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        SpreadsheetsResource.ValuesResource _googleSheetValues;
        //const string SPREADSHEET_ID = "1IwYu6ViZn_4gTN_sNr0ByXW2nOR9QFy_uB3omTUue1A";
        List<string> spreadSheetColumns = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        "AA","AB","AC","AD","AE","AF","AG","AH","AI"};
        //const string SHEET_NAME = "Items";

        public LeadController(IUnitOfWork uow , IMapper mapper, GoogleSheetsHelper googleSheetsHelper)
        {
            _uow = uow;
            _mapper = mapper;
            _googleSheetValues = googleSheetsHelper.Service.Spreadsheets.Values;

        }
        [HttpGet("getLeadsFromDb")]
        public async Task<ActionResult> getLeadsFromDb(int serviceProfileId) {
            var leads = await _uow._leadService.GetLeadByServiceProfile(serviceProfileId);
            var leadsDto = _mapper.Map<List<LeadDto>>(leads);
            return Ok(leadsDto);
        }

        [HttpGet("getDeals")]
        public async Task<ActionResult> getDeals(int profileId)
        {
            var lead = await _uow._leadService.GetLeadByServiceProfile(profileId);
            if(lead == null)
            {
                return NotFound(new ApiResponse(404 , "there is no lead for the service yet"));
            }
            //var range = $"{SHEET_NAME}!A:D";
            try
            {
                var values = getSheetData(lead.sheetIdentifier);
                if (values.Count > 0)
                    values = values.Where(x => x.Count > values[0].Count).ToList();
                //to do filter here
                return Ok(values);
            }
            catch (Exception ex) {
                return BadRequest(new ApiResponse(500, ex.Message));
            }
        }

        private IList<IList<Object>> getSheetData(string sheetIdentifier)
        {
            var sheetId = GetUrlIdentifier(sheetIdentifier);
            string range = getRange(sheetId);
            var request = _googleSheetValues.Get(sheetId, range);
            var response = request.Execute();
            var values = response.Values;
            return values;
        }

        private string getRange(string sheetId)
        {
            int countOfColumns = getCountOfColumns(sheetId);
            string y = spreadSheetColumns[countOfColumns+1];
            return $"!A:{y}";
        }

        private int getCountOfColumns(string sheetId)
        {
            var range = $"!A{1}:AZ{1}";
            var request = _googleSheetValues.Get(sheetId, range);
            var response = request.Execute();
            var values = response.Values;
            return values[0].Count;
        }

        [HttpPost("addProfileLead")]
        public async Task<ActionResult> addProfileLead(AddLeadDto model)
        {
            var service = await _uow._serviceProfile.GetServiceProfileWithLead(model.ServiceProfileId);
            if(service == null)
            {
                return NotFound(new ApiResponse(404, "Service profile not exist"));
            }
            if(service.Leads.Count > 0)
            {
                return NotFound(new ApiResponse(404, "Service has already lead"));
            }
            if(await _uow._leadService.IsSheetIdExists(model.sheetUrl))
            {
                return BadRequest(new ApiResponse(400, "Sheet is alread assigned to another service"));
            }
            try
            {
                isGoogleSheetGood(model.sheetUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(500, ex.Message));
            }
            var lead = new Lead()
            {
                ServiceProfileId = model.ServiceProfileId,
                sheetIdentifier = model.sheetUrl
            };
            await _uow._leadService.AddLead(lead);
            await _uow.saveChanges();
            var leadDto = _mapper.Map<LeadDto>(lead);
            return Ok(leadDto);
        }

        private void isGoogleSheetGood(string sheetUrl)
        {
          string sheetId = GetUrlIdentifier(sheetUrl);
          var request = _googleSheetValues.Get(sheetId, "!A1:A1");
          var response = request.Execute();
          var values = response.Values;  
        }

        private string GetUrlIdentifier(string url)
        {
            try
            {
                var id = url.Split("/d/")[1].Split("/")[0];
                return id;
            }
            catch
            {
                throw new Exception("spreadsheet url is not correct");
            }
        }

        [HttpPut("UpdateLead")]
        public async Task<ActionResult> UpdateLead(int id,LeadDto model)
        {
            var lead = await _uow._leadService.GetLeadById(id);
            if(lead.Id != model.Id)
            {
                return BadRequest(new ApiResponse(400, "lead Id is not correct"));
            }
            try
            {
                isGoogleSheetGood(model.sheetIdentifier);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(500, ex.Message));
            }
            lead.sheetIdentifier = model.sheetIdentifier;
            await _uow.saveChanges();
            return Ok(lead);
        }

        [HttpPut("updateGoogleSheet/{profileId}/{rowId}")]
        public async Task<ActionResult>updateGoogleSheet(int rowId,int profileId, List<string> status)
        {
            var lead = await _uow._leadService.GetLeadByServiceProfile(profileId);
            var sheetId = GetUrlIdentifier(lead.sheetIdentifier);
            var range = $"!A{rowId}:{spreadSheetColumns[status.Count+ 1]}{rowId}";

            var objectList = new List<object>();
            foreach(var str in status)
            {
                objectList.Add(str);
            }
            var rangeData = new List<IList<object>> { objectList };

            var valueRange = new ValueRange
            {
                Values = rangeData
            };
            var updateRequest = _googleSheetValues.Update(valueRange, sheetId, range);
            updateRequest.ValueInputOption = UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
            return NoContent();
        }

       
    }
}
