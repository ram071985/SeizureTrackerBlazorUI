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

        #endregion

        #region Public fields

        public event Action? OnChange;

        #endregion

        #region Properties

        public List<string> SeizureType => _seizureTypes;
        
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

        public StateContainer()
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
        }

        #endregion


        #region Private methods

        private void NotifyStateChanged() => OnChange?.Invoke();

        #endregion
    }
}