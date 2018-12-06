using Common.Clases;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ejemplo.Web
{
    public partial class vUsuarioMostrar : System.Web.UI.Page
    {
        static List<cUsuario> lstUsuariosActivos;
        static List<cUsuario> lstUsuariosCBActivos;
        static List<cUsuario> lstUsuariosBActivos;
        static List<cUsuario> ListarActivos;

        static List<cUsuario> lstUsuariosInactivos;
        static List<cUsuario> lstUsuariosCBInactivos;
        static List<cUsuario> lstUsuariosBInactivos;
        static List<cUsuario> ListarInactivos;

        static bool FiltroPorBusqueda;
        static bool FiltroPorCheckBox;

        //TATO ME VES?


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lstUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
                lstUsuariosCBActivos = new List<cUsuario>();
                lstUsuariosBActivos = new List<cUsuario>();
                ListarActivos = new List<cUsuario>();

                lstUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
                lstUsuariosCBInactivos = new List<cUsuario>();
                lstUsuariosBInactivos = new List<cUsuario>();
                ListarInactivos = new List<cUsuario>();

                lstUsuariosBActivos = lstUsuariosActivos;//POR AHORA
                lstUsuariosBInactivos = lstUsuariosInactivos;//POR AHORA
                lstUsuariosCBActivos = lstUsuariosActivos;
                lstUsuariosCBInactivos = lstUsuariosInactivos;


                grdUsuariosActivos.DataSource = dFachada.UsuarioTraerTodosActivos();
                grdUsuariosActivos.DataBind();
                grdUsuariosInactivos.DataSource = dFachada.UsuarioTraerTodosInactivos();
                grdUsuariosInactivos.DataBind();

                FiltroPorBusqueda = false;
                FiltroPorCheckBox = false;
                //CargarGrillas();
            }
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            #region
            /*lstUsuariosActivos = dFachada.TraerTodosActivosUsuario();
            lstUsuariosInactivos = dFachada.TraerTodosInactivosUsuario();
            lstUsuariosCBActivos = new List<cUsuario>();
            lstUsuariosCBInactivos = new List<cUsuario>();

            List<cUsuario> listaActivos;
            List<cUsuario> listaInactivos;

            if (estado)
            {
                listaActivos = new List<cUsuario>();
                listaInactivos = new List<cUsuario>();
                listaActivos = lstUsuariosBActivos;
                listaInactivos = lstUsuariosBInactivos;
                estado = false;
            }*/
            #endregion
            #region obtengo la lista de usuarios CB activos e inactivos cargadas
            /*
            #region Sin filtro por busqueda
            if (!FiltroPorBusqueda)
            {

                if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked && !cbAdministrador.Checked &&
                    !cbAdministrativo.Checked && !cbEspecialista.Checked)
                {
                    lstUsuariosCBActivos = lstUsuariosActivos;
                    lstUsuariosCBInactivos = lstUsuariosInactivos;
                }
                else
                {
                    if (this.cbFisioterapeuta.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }
                    }
                    if (this.cbFonoaudiologo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Fonoudiologia")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Fonoudiologia")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }
                    }
                    if (this.cbPedagogo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }
                    }
                    if (this.cbPsicologo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }
                    }
                    if (this.cbPsicomotricista.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }

                    }
                    if (this.cbSinEspecialidad.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                            }
                        }
                    }
                    if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked)
                    {
                        if (this.cbAdministrador.Checked)
                        {
                            for (int i = 0; i < lstUsuariosActivos.Count; i++)
                            {
                                if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBActivos.Count; a++)
                                    {
                                        if (lstUsuariosCBActivos[a].NickName == lstUsuariosActivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                            {
                                if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBInactivos.Count; a++)
                                    {
                                        if (lstUsuariosCBInactivos[a].NickName == lstUsuariosInactivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                            }
                        }
                        if (this.cbAdministrativo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosActivos.Count; i++)
                            {
                                if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBActivos.Count; a++)
                                    {
                                        if (lstUsuariosCBActivos[a].NickName == lstUsuariosActivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                            {
                                if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBInactivos.Count; a++)
                                    {
                                        if (lstUsuariosCBInactivos[a].NickName == lstUsuariosInactivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                            }
                        }
                        if (this.cbEspecialista.Checked)
                        {
                            for (int i = 0; i < lstUsuariosActivos.Count; i++)
                            {
                                if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBActivos.Count; a++)
                                    {
                                        if (lstUsuariosCBActivos[a].NickName == lstUsuariosActivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                            {
                                if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {
                                    bool existe = false;
                                    for (int a = 0; a < lstUsuariosCBInactivos.Count; a++)
                                    {
                                        if (lstUsuariosCBInactivos[a].NickName == lstUsuariosInactivos[i].NickName)
                                        {
                                            existe = true;
                                        }
                                    }
                                    if (!existe)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.cbAdministrador.Checked)
                        {
                            for (int i = 0; i < lstUsuariosCBActivos.Count; i++)
                            {
                                if (lstUsuariosCBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosCBInactivos.Count; i++)
                            {
                                if (lstUsuariosCBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbAdministrativo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosCBActivos.Count; i++)
                            {
                                if (lstUsuariosCBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosCBInactivos.Count; i++)
                            {
                                if (lstUsuariosCBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBInactivos.RemoveAt(i);
                                }

                            }
                        }
                        if (this.cbEspecialista.Checked)
                        {
                            for (int i = 0; i < lstUsuariosCBActivos.Count; i++)
                            {
                                if (lstUsuariosCBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosCBInactivos.Count; i++)
                            {
                                if (lstUsuariosCBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {

                                }
                                else
                                {
                                    lstUsuariosCBInactivos.RemoveAt(i);
                                }
                            }
                        }
                    }

                }
            }
            #endregion
            #region Con filtro por busqueda

            else
            {
                if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked && !cbAdministrador.Checked &&
                    !cbAdministrativo.Checked && !cbEspecialista.Checked)
                {
                    lstUsuariosCBActivos = listaActivos; 
                    lstUsuariosCBInactivos = listaInactivos;
                }
                else
                {

                    if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked)
                    {
                        #region Si no tiene filtro por especialidad - SOLO POR TIPO DE USUARIO
                        if (this.cbAdministrador.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {

                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                {

                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbAdministrativo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {

                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                {

                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbEspecialista.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {

                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }

                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                {

                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                    }
                    #endregion

                    else if (!cbAdministrador.Checked && !cbAdministrativo.Checked && !cbEspecialista.Checked)
                    {
                        #region Si no tiene filtro por tipo de usuario - SOLO ESPECIALIDAD

                        if (this.cbFisioterapeuta.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Fisioterapia")
                                {

                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Fisioterapia")
                                {
                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbFonoaudiologo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Fonoudiologia")
                                {

                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Fonoudiologia")
                                {
                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbPedagogo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Pedadogia")
                                {
                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Pedadogia")
                                {
                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbPsicologo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Psicologia")
                                {
                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Psicologia")
                                {

                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        if (this.cbPsicomotricista.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Psicomotricidad")
                                {
                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Psicomotricidad")
                                {
                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }

                        }
                        if (this.cbSinEspecialidad.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Sin especialidad")
                                {
                                }
                                else
                                {
                                    lstUsuariosBActivos.RemoveAt(i);
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Sin especialidad")
                                {
                                }
                                else
                                {
                                    lstUsuariosBInactivos.RemoveAt(i);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Si tiene filtros por especialidad y tipo de usuario

                        if (this.cbFisioterapeuta.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Fisioterapia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Fisioterapia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }

                        }




                        if (this.cbFonoaudiologo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Fonoudiologia")
                                {

                                }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Fonoudiologia")
                                {
                                }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                        if (this.cbPedagogo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Pedadogia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Pedadogia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                        if (this.cbPsicologo.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Psicologia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Psicologia")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                        if (this.cbPsicomotricista.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Psicomotricidad")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Psicomotricidad")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }

                        }
                        if (this.cbSinEspecialidad.Checked)
                        {
                            for (int i = 0; i < lstUsuariosBActivos.Count; i++)
                            {
                                if (lstUsuariosBActivos[i].Especialidad.Nombre == "Sin especialidad")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBActivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < lstUsuariosBInactivos.Count; i++)
                            {
                                if (lstUsuariosBInactivos[i].Especialidad.Nombre == "Sin especialidad")
                                { }
                                else
                                {
                                    if (this.cbAdministrador.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbAdministrativo.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                    if (this.cbEspecialista.Checked)
                                    {
                                        if (lstUsuariosBInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                        { }
                                        else
                                        {
                                            lstUsuariosBInactivos.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion
            */
            #endregion
            #region 
            /* if (lstUsuariosActivos.Count == lstUsuariosBActivos.Count) //si la lista de busqueda es igual a la completa, se listan lo que estan en la lista de checkbox ya que cuenta con usuarios filtrados
             {
                 ListarActivos = lstUsuariosCBActivos;
             }
             else if (lstUsuariosActivos.Count == lstUsuariosCBActivos.Count)//si la lista de checkbox es igual a la completa, se listan lo que estan en la lista de busqueda ya que cuenta con usuarios filtrados
             {
                 ListarActivos = lstUsuariosBActivos;
             }

             if (lstUsuariosInactivos.Count == lstUsuariosBInactivos.Count)
             {
                 ListarInactivos = lstUsuariosCBInactivos;
             }
             else if (lstUsuariosInactivos.Count == lstUsuariosCBInactivos.Count)
             {
                 ListarInactivos = lstUsuariosBInactivos;
             }

             if (lstUsuariosBActivos.Count < lstUsuariosActivos.Count && lstUsuariosCBActivos.Count < lstUsuariosActivos.Count)
             {
                 for (int i = 0; i < lstUsuariosCBActivos.Count; i++)
                 {
                     for (int j = 0; j < lstUsuariosBActivos.Count; j++)
                     {
                         if (lstUsuariosCBActivos[i].NickName == lstUsuariosBActivos[j].NickName)
                         {
                             ListarActivos.Add(lstUsuariosCBActivos[i]);
                         }
                     }
                 }
             }
             if (lstUsuariosBInactivos.Count < lstUsuariosInactivos.Count && lstUsuariosCBInactivos.Count < lstUsuariosInactivos.Count)
             {
                 for (int i = 0; i < lstUsuariosCBInactivos.Count; i++)
                 {
                     for (int j = 0; j < lstUsuariosBInactivos.Count; j++)
                     {
                         if (lstUsuariosCBInactivos[i].NickName == lstUsuariosBInactivos[j].NickName)
                         {
                             ListarActivos.Add(lstUsuariosCBInactivos[i]);
                         }
                     }
                 }
             }*/
            #endregion

            lstUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
            lstUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
            lstUsuariosCBActivos = new List<cUsuario>();
            lstUsuariosCBInactivos = new List<cUsuario>();

            if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked && !cbAdministrador.Checked &&
                    !cbAdministrativo.Checked && !cbEspecialista.Checked)
            {
                if(FiltroPorBusqueda)
                {
                    ListarActivos = lstUsuariosBActivos;
                    ListarInactivos = lstUsuariosBInactivos;
                    FiltroPorCheckBox = false;
                }
                else
                {
                    lstUsuariosCBActivos = lstUsuariosActivos;
                    lstUsuariosCBInactivos = lstUsuariosInactivos;
                    ListarActivos = lstUsuariosCBActivos;
                    ListarInactivos = lstUsuariosCBInactivos;
                    FiltroPorCheckBox = false;
                }
                
            }
            else
            {
                FiltroPorCheckBox = true;
                #region Agrego a la lstUsuariosCBActivos e Inactivos los usuarios que tengan la especialidad y tipo de usuario deseada

                if (cbFisioterapeuta.Checked || cbFonoaudiologo.Checked || cbPedagogo.Checked || cbPsicologo.Checked || cbPsicomotricista.Checked || cbSinEspecialidad.Checked)
                {
                    #region cbFisioterapeuta
                    if (this.cbFisioterapeuta.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if(!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }

                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Fonoaudiologo
                    if (this.cbFonoaudiologo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Fonoudiologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Fonoudiologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Pedadogo
                    if (this.cbPedagogo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Psicologo
                    if (this.cbPsicologo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Psicomotricista
                    if (this.cbPsicomotricista.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }

                    }
                    #endregion
                    #region SinEspecialidad
                    if (this.cbSinEspecialidad.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < lstUsuariosInactivos.Count; i++)
                        {
                            if (lstUsuariosInactivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (lstUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    lstUsuariosCBInactivos.Add(lstUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    if (this.cbAdministrador.Checked)
                    {
                        for(int i=0; i<lstUsuariosActivos.Count;i++)
                        {
                            if(lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for(int j=0; j<lstUsuariosInactivos.Count; j++)
                        {
                            if(lstUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[j]);
                            }
                        }
                    }
                    if (this.cbAdministrativo.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int j = 0; j < lstUsuariosInactivos.Count; j++)
                        {
                            if (lstUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[j]);
                            }
                        }
                    }
                    if (this.cbEspecialista.Checked)
                    {
                        for (int i = 0; i < lstUsuariosActivos.Count; i++)
                        {
                            if (lstUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                            {
                                lstUsuariosCBActivos.Add(lstUsuariosActivos[i]);
                            }
                        }
                        for (int j = 0; j < lstUsuariosInactivos.Count; j++)
                        {
                            if (lstUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                            {
                                lstUsuariosCBInactivos.Add(lstUsuariosInactivos[j]);
                            }
                        }
                    }
                }
                #endregion

                List<cUsuario> activos = new List<cUsuario>();
                List<cUsuario> inactivos = new List<cUsuario>();

                if (FiltroPorBusqueda)
                {
                    for(int a=0; a< lstUsuariosBActivos.Count; a++)
                    {
                        for(int b=0; b<lstUsuariosCBActivos.Count; b++)
                        {
                            if(lstUsuariosBActivos[a].NickName==lstUsuariosCBActivos[b].NickName)
                            {
                                activos.Add(lstUsuariosBActivos[a]);
                            }
                            else
                            {
                                //ListarActivos.RemoveAt(a);
                            }
                        }
                    }
                    for (int a = 0; a < lstUsuariosBInactivos.Count; a++)
                    {
                        for (int b = 0; b < lstUsuariosCBInactivos.Count; b++)
                        {
                            if (lstUsuariosBInactivos[a].NickName == lstUsuariosCBInactivos[b].NickName)
                            {
                                inactivos.Add(lstUsuariosBInactivos[a]);
                            }
                            else
                            {
                                //ListarInactivos.RemoveAt(a);
                            }
                        }
                    }
                    ListarActivos = activos;
                    ListarInactivos = inactivos;

                }
                else
                {
                    ListarActivos = lstUsuariosCBActivos;
                    ListarInactivos = lstUsuariosCBInactivos;
                }
            }
            CargarGrillas();
        }


        private void CargarGrillas()
        {
            this.grdUsuariosActivos.DataSource = null;
            this.grdUsuariosActivos.DataBind();
            this.grdUsuariosInactivos.DataSource = null;
            this.grdUsuariosInactivos.DataBind();

            this.grdUsuariosActivos.DataSource = ListarActivos;
            this.grdUsuariosActivos.DataBind();
            this.grdUsuariosActivos.SelectedIndex = -1;
            this.grdUsuariosInactivos.DataSource = ListarInactivos;
            this.grdUsuariosInactivos.DataBind();
            this.grdUsuariosInactivos.SelectedIndex = -1;
            if (ListarInactivos.Count == 0)
            {
                this.PanelUsuariosInactivos.Visible = false;
                this.lblUsuariosInactivos.Text = "No hay usuarios inactivos";
            }
            else
            {
                this.PanelUsuariosInactivos.Visible = true;
                this.lblUsuariosInactivos.Text = "Usuarios inactivos";
            }
            if (ListarActivos.Count == 0)
            {
                this.PanelUsuariosActivos.Visible = false;
                this.lblUsuariosActivos.Text = "No hay usuarios activos";
            }
            else
            {
                this.PanelUsuariosActivos.Visible = true;
                this.lblUsuariosActivos.Text = "Usuarios activos";
            }
        }

        protected void txtBuscarBeneficiario_TextChanged(object sender, EventArgs e)
        {
            #region obtendo lista de usuario B activos e inactivos cargada
            /*if (texto == "")
            {
                lstUsuariosBActivos = lstUsuariosActivos;
                lstUsuariosBInactivos = lstUsuariosInactivos;
                FiltroPorBusqueda = false;
            }
            else
            {
                FiltroPorBusqueda = true;
                int val = 0;
                if (int.TryParse(txtBuscarBeneficiario.Text, out val))
                {
                    // es numerico
                    lstUsuariosBActivos = dFachada.TraerTodosActivosPorCI(texto);
                    lstUsuariosBInactivos = dFachada.TraerTodosInactivosPorCI(texto);
                }
                else
                {
                    //no lo es 
                    lstUsuariosBActivos = dFachada.TraerTodosActivosPorNombreApellidoUsuario(texto);
                    lstUsuariosBInactivos = dFachada.TraerTodosInactivosPorNombreApellidoUsuario(texto);
                }
            }*/
            #endregion         

            string texto = txtBuscarBeneficiario.Text;
            lstUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
            lstUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
            lstUsuariosBActivos = new List<cUsuario>();
            lstUsuariosBInactivos = new List<cUsuario>();

            if (texto == "")
            {
                lstUsuariosBActivos = lstUsuariosActivos;
                lstUsuariosBInactivos = lstUsuariosInactivos;
                FiltroPorBusqueda = false;
                if(FiltroPorCheckBox)
                {
                    ListarActivos = lstUsuariosCBActivos;
                    ListarInactivos = lstUsuariosCBInactivos;
                }
                else
                {
                    ListarActivos = lstUsuariosBActivos;
                    ListarInactivos = lstUsuariosBInactivos;
                }
            }
            else
            {
                FiltroPorBusqueda = true;
                int val = 0;
                if (int.TryParse(txtBuscarBeneficiario.Text, out val))
                {
                    // es numerico
                    lstUsuariosBActivos = dFachada.UsuarioTraerTodosActivosPorCI(texto);
                    lstUsuariosBInactivos = dFachada.UsuarioTraerTodosInactivosPorCI(texto);
                }
                else
                {
                    //no lo es 
                    lstUsuariosBActivos = dFachada.UsuarioTraerTodosActivosPorNombreApellido(texto);
                    lstUsuariosBInactivos = dFachada.UsuarioTraerTodosInactivosPorNombreApellido(texto);
                }
                if (FiltroPorCheckBox)
                {
                    ListarActivos = lstUsuariosCBActivos;
                    ListarInactivos = lstUsuariosCBInactivos;

                    List<cUsuario> activos = new List<cUsuario>();
                    List<cUsuario> inactivos = new List<cUsuario>();


                    if (lstUsuariosBActivos.Count > 0 && ListarActivos.Count > 0)
                    {
                        for (int i = 0; i < ListarActivos.Count; i++)
                        {
                            for (int j = 0; j < lstUsuariosBActivos.Count; j++)
                            {
                                if (ListarActivos[i].NickName == lstUsuariosBActivos[j].NickName)
                                {
                                    activos.Add(ListarActivos[i]);
                                }
                                else
                                {
                                    //ListarActivos.RemoveAt(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        ListarActivos = new List<cUsuario>();
                    }

                    if (lstUsuariosBInactivos.Count > 0 && ListarInactivos.Count > 0)
                    {
                        for (int i = 0; i < ListarInactivos.Count; i++)
                        {
                            for (int j = 0; j < lstUsuariosBInactivos.Count; j++)
                            {
                                if (ListarInactivos[i].NickName == lstUsuariosBInactivos[j].NickName)
                                {
                                    inactivos.Add(ListarInactivos[i]);
                                }
                                else
                                {
                                    //ListarInactivos.RemoveAt(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        ListarInactivos = new List<cUsuario>();
                    }
                    ListarActivos = activos;
                    ListarInactivos = inactivos;
                }
                else
                {
                    ListarActivos = lstUsuariosBActivos;
                    ListarInactivos = lstUsuariosBInactivos;
                }



            }



            CargarGrillas();
        }



        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            #region
            FiltroPorCheckBox = false;
            if(FiltroPorBusqueda)
            {
                ListarActivos = lstUsuariosBActivos;
                ListarInactivos = lstUsuariosBInactivos;
            }
            else
            {
                ListarActivos = lstUsuariosActivos;
                ListarInactivos = lstUsuariosInactivos;
            }
        

            //ListarActivos = lstUsuariosActivos;
            //ListarInactivos = lstUsuariosInactivos;
            this.cbFisioterapeuta.Checked = false;
            this.cbAdministrador.Checked = false;
            this.cbAdministrativo.Checked = false;
            this.cbEspecialista.Checked = false;
            this.cbFonoaudiologo.Checked = false;
            this.cbPedagogo.Checked = false;
            this.cbPsicologo.Checked = false;
            this.cbPsicomotricista.Checked = false;
            this.cbSinEspecialidad.Checked = false;
            CargarGrillas();
            #endregion

        }


        #region Extras

        protected void grdUsuariosActivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaNickName = grdUsuariosActivos.Rows[e.NewSelectedIndex].Cells[2];
            string NickName = string.Format(celdaNickName.Text);
            if (vMiPerfil.U.NickName == NickName)
            {
                this.grdUsuariosActivos.SelectedIndex = 3;
                ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('ERROR: Ingresa a tu perfil para ver detalles personales.')", true);

            }
            else
            {
                Response.Redirect("vUsuarioDetalles.aspx?nickname=" + NickName);
            }
        }

        protected void grdUsuariosInactivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaNickName = grdUsuariosInactivos.Rows[e.NewSelectedIndex].Cells[2];
            string NickName = string.Format(celdaNickName.Text);
            Response.Redirect("vUsuarioDetalles.aspx?nickname=" + NickName);
        }

        protected void grdUsuariosActivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int i = grdUsuariosActivos.PageCount;
            if (i > 0)
            {
                e.Row.Cells[1].Visible = false; //codigo
                e.Row.Cells[3].Visible = false; //contraseña
                e.Row.Cells[12].Visible = false;//estado
            }
        }

        protected void grdUsuariosInactivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            int i = grdUsuariosInactivos.PageCount;
            if (i > 0)
            {
                e.Row.Cells[1].Visible = false; //codigo
                e.Row.Cells[3].Visible = false; //contraseña
                e.Row.Cells[12].Visible = false;//estado
            }

        }

        #endregion
    }
}