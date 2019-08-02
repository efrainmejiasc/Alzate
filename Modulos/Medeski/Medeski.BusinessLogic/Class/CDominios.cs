using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medeski.DataAcces.Class;
using System.DirectoryServices;

namespace Medeski.BusinessLogic.Class
{
    public class CDominios : Interfase.IDominios
    {
        private readonly IDominios CRUD;

        public CDominios()
        {
            CRUD = new Dominios();   
        }
        
        public IList<GE_TDOMINIOS> GetAll()
        {
            try
            {
                return CRUD.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public void Remove(params GE_TDOMINIOS[] objeto)
        {
            try
            {
                CRUD.Remove(objeto);
            }
            catch
            {
                throw;
            }
        }

        public IList<GE_TDOMINIOS> GruposDirectorioActivo()
        {

            try
            {
                CVlrsparamgrales parametro = new CVlrsparamgrales();
                string paths = parametro.GetByClase("PATHLDAP").vhpg_valor;
                return GetGroups(paths);

            }
            catch (Exception)
            {

                return null;
            }

        }

        public IList<GE_TDOMINIOS> GetGroups(string path)
        {
            DirectoryEntry objADAM = default(DirectoryEntry);
            // Binding object. 
            DirectoryEntry objGroupEntry = default(DirectoryEntry);
            // Group Results. 
            DirectorySearcher objSearchADAM = default(DirectorySearcher);
            // Search object. 
            SearchResultCollection objSearchResults = default(SearchResultCollection);
            // Results collection. 
            string strPath = null;
            // Binding path. 
            List<GE_TDOMINIOS> result = new List<GE_TDOMINIOS>();

            // Construct the binding string. 

            strPath = path;
            //Change to your ADserver 

            // Get the AD LDS object. 
            try
            {
                objADAM = new DirectoryEntry(strPath);
                objADAM.RefreshCache();
            }
            catch (Exception e)
            {
                throw e;
            }

            // Get search object, specify filter and scope, 
            // perform search. 
            try
            {
                objSearchADAM = new DirectorySearcher(objADAM);
                objSearchADAM.PageSize = 5000;
                objSearchADAM.Filter = "(&(objectClass=group))";
                objSearchADAM.SearchScope = SearchScope.Subtree;
                objSearchResults = objSearchADAM.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }

            // Enumerate groups 
            try
            {


                if (objSearchResults.Count != 0)
                {
                    foreach (SearchResult objResult in objSearchResults)
                    {
                        objGroupEntry = objResult.GetDirectoryEntry();
                        GE_TDOMINIOS dominios = new GE_TDOMINIOS();
                        dominios.domi_nombre = "fanalcasa";//objGroupEntry.Username.Split('\\')[0];
                        dominios.domi_grupo = objGroupEntry.Name.Split('=')[1];
                        dominios.domi_fecha = DateTime.Today;
                        dominios.domi_estado = 1;
                        result.Add(dominios);
                    }
                }
                else
                {
                    throw new Exception("No groups found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public int InsertAll()
        {
            try
            {
                int cont = 0;
                IList<GE_TDOMINIOS> dom = GetAll();

                if (dom.Count > 0)
                {
                    foreach (GE_TDOMINIOS d in dom)
                    {
                        Remove(d);
                        cont++;
                    }
                }

                if (cont > 0)
                {

                    IList<GE_TDOMINIOS> grupos = GruposDirectorioActivo();

                    int i = 1;

                    foreach (GE_TDOMINIOS c in grupos)
                    {
                        GE_TDOMINIOS domi = new GE_TDOMINIOS();
                        domi.domi_consecutivo = i;
                        domi.domi_estado = 1;
                        domi.domi_fecha = DateTime.Today;
                        domi.domi_nombre = c.domi_nombre;
                        domi.domi_grupo = c.domi_grupo;
                        i++;
                        Add(domi);
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Add(params GE_TDOMINIOS[] objeto)
        {
            try
            {
                CRUD.Add(objeto);
            }
            catch
            {
                throw;
            }
        }
    }
}
