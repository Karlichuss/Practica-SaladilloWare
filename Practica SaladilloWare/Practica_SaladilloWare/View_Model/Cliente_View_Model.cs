using System;
using System.Collections;
using Practica_SaladilloWare.Model;
using Practica_SaladilloWare.Assets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Practica_SaladilloWare.View_Model
{
    public class Cliente_View_Model
    {
        public Usuario usuario;

        public Cliente_View_Model(Usuario usuario)
        {
            this.usuario = usuario;
        }

        public async Task<List<String>> GetPlacasBase()
        {
            return await PlacaBase_Repository.GetNombres();
        }

        public async Task<List<String>> GetProcesadores()
        {
            return await Procesador_Repository.GetNombres();
        }

        public async Task<List<String>> GetChasis()
        {
            return await Chasis_Repository.GetNombres();
        }

        public async Task<List<String>> GetRAMs()
        {
            return await RAM_Repository.GetNombres();
        }

        public async Task<List<String>> GetTarjetasGraficas()
        {
            return await TarjetaGrafica_Repository.GetNombres();
        }
    }
}
