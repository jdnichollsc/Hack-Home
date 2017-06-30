using HackHome.Entities;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace HackHome.SAL
{
    public class MicrosoftServiceClient
    {
        // Cliente para acceder al servicio Mobile
        MobileServiceClient Client;
        // Objeto para realizar operaciones con Tablas de Mobile Service
        private IMobileServiceTable<LabItem> LabItemTable;

        /// <summary>
        /// Envia una evidencia.
        /// </summary>
        /// <param name="userEvidence">Objeto con los datos de la evidencia.</param>
        /// <returns></returns>
        public async Task SendEvidence(LabItem userEvidence)
        {
            Client =
                new MobileServiceClient(@"http://xamarin-diplomado.azurewebsites.net/");
            LabItemTable = Client.GetTable<LabItem>();
            await LabItemTable.InsertAsync(userEvidence);
        }
    }
}