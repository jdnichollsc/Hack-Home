using Android.App;
using Android.Views;
using Android.Widget;
using HackHome.Entities;
using System.Collections.Generic;

namespace HackHome.CustomAdapters
{
    /// <summary>
    /// EvidenceAdapter es un tipo Adapter que permite administrar los datos de un ListView.
    /// Los datos que maneja son de tipo Evidence.
    /// </summary>
    public class EvidencesAdapter : BaseAdapter<Evidence>
    {
        List<Evidence> Items; // Datos de cada evidencia de laboratorio.
        Activity Context; // Activity donde se utilizará este Adapter.
        int ItemLayoutTemplate; // Layout a utilizar para mostrar los datos de un elemento.
        int EvidenceTitleViewID; // ID del TextView donde se mostrará el nombre de la evidencia.
        int EvidenceStatusViewID; // ID del TextView donde se mostrará el estatus de la evidencia.

        /// <summary>
        /// Constructor para recibir la información que necesita el Adapter
        /// </summary>
        /// <param name="context">Activity donde se aloja el ListView.</param>
        /// <param name="evidences">La lista de elementos.</param>
        /// <param name="itemLayoutTemplate">ID del Layout para mostrar cada elemento del ListView.</param>
        /// <param name="evidenceTitleViewID">ID del TextView donde se mostrará el título de la evidencia.</param>
        /// <param name="evidenceStatusViewID">ID del TextView donde se mostrará el estatus de la evidencia.</param>
        public EvidencesAdapter(Activity context, List<Evidence> evidences, int itemLayoutTemplate, int evidenceTitleViewID, int evidenceStatusViewID)
        {
            this.Context = context;
            this.Items = evidences;
            this.ItemLayoutTemplate = itemLayoutTemplate;
            this.EvidenceTitleViewID = evidenceTitleViewID;
            this.EvidenceStatusViewID = evidenceStatusViewID;
        }

        /// <summary>
        ///  Devuelve el elemento de la lista localizado en la posición especificada.
        /// </summary>
        /// <param name="position">Posición del elemento dentro de la lista.</param>
        /// <returns></returns>
        public override Evidence this[int position]
        {
            get
            {
                return Items[position];
            }
        }

        /// <summary>
        /// Devuelve el número de elementos de la lista.
        /// </summary>
        public override int Count
        {
            get
            {
                return Items.Count;
            }
        }

        /// <summary>
        /// Devuelve el ID del elemento localizado en la posición especificada.
        /// </summary>
        /// <param name="position">Posición del elemento dentro de la lista.</param>
        /// <returns></returns>
        public override long GetItemId(int position)
        {
            return Items[position].EvidenceID; 
        }

        //  
        /// <summary>
        /// Devuelve el View que muestra los datos de un elemento del conjunto de datos.
        /// </summary>
        /// <param name="position">Posición del elemento a mostrar.</param>
        /// <param name="convertView">View anterior que puede ser reutilizada.</param>
        /// <param name="parent">View padre al que podria adjuntarse el View devuelto.</param>
        /// <returns></returns>
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Obtenemos el elemento del cual se requiere la Vista
            var Item = Items[position];
            View ItemView; // Vista que vamos a devolver
            if(convertView == null)
            {
                // No hay vista reutilizable, crear una nueva
                ItemView = Context.LayoutInflater.Inflate(ItemLayoutTemplate, null /* No hay View padre*/);
            }
            else
            {
                // Reutilizamos un View existente para ahorrar recursos
                ItemView = convertView;
            }

            // Establecemos los datos del elemento dentro del View
            ItemView.FindViewById<TextView>(EvidenceTitleViewID).Text = Item.Title;
            ItemView.FindViewById<TextView>(EvidenceStatusViewID).Text = Item.Status;

            return ItemView;
        }
    }
}
