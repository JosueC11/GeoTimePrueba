using GeotimeNet.Client.Modelo;
using GeotimeNet.Client.Modelo.FromApi;

namespace GeotimeNet.Client.Data.State
{
    internal class LoginState
    {
        public cEmpleado Empleado { get; set; }

        public event Action OnChange;

        public LoginState()
        {
            Empleado = null;
        }


        public void SetLogin(cEmpleado empleado)
        {
            Empleado = empleado;
            NotifyStateChanged();
        }
        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
