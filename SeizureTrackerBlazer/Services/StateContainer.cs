using System.Text.Json;
using SeizureTrackerBlazer.Constants;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using SeizureTrackerBlazer.Models;

namespace SeizureTrackerBlazer.Services
{
    public class StateContainer
    {
        #region Private fields

        private List<string> _seizureTypes;
        private EditContext? _editContext;
        private List<string> _medicationChange;
        private SeizureTrackerService _seizureTrackerService;

        #endregion

        #region Public fields

        public event Action? OnChange;

        #endregion

        #region Properties

        public List<string> SeizureType => _seizureTypes;
        public List<string> MedicationChange => _medicationChange;

        public EditContext? EditContext
        {
            get => _editContext;
            set
            {
                if (value != _editContext)
                {
                    _editContext = value;
                    NotifyStateChanged();
                }
            }
        }

        #endregion

        #region Construction

        public StateContainer(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _seizureTypes = new List<string>
            {
                { SeizureTypes.Partial },
                { SeizureTypes.Complex },
                { SeizureTypes.Aura },
                { SeizureTypes.GrandMal },
                { SeizureTypes.Musicogenic },
                { SeizureTypes.SmellSensory },
                { SeizureTypes.HeatSensory },
                { SeizureTypes.Anxiety },
                { SeizureTypes.MedicationChange }
            };

            _medicationChange = new List<string>
            {
                { MedChange.Yes },
                { MedChange.No },
            };

            _seizureTrackerService = new SeizureTrackerService(config, httpClientFactory);
        }

        public async Task AddSeizureActivityLog(SeizureActivityDetail seizureActivityLog)
        {
            try
            {
                await _seizureTrackerService.AddSeizureActivityLog(JsonSerializer.Serialize(seizureActivityLog));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        #endregion


        #region Private methods

        private void NotifyStateChanged() => OnChange?.Invoke();

        #endregion
    }
}