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
        static List<cUsuario> LosUsuariosActivos;
        static List<cUsuario> LosUsuariosCBActivos;
        static List<cUsuario> LosUsuariosBActivos;
        static List<cUsuario> LosListarActivos;

        static List<cUsuario> LosUsuariosInactivos;
        static List<cUsuario> LosUsuariosCBInactivos;
        static List<cUsuario> LosUsuariosBInactivos;
        static List<cUsuario> LosListarInactivos;

        static bool BFiltroPorBusqueda;
        static bool BFiltroPorCheckBox;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LosUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
                LosUsuariosCBActivos = new List<cUsuario>();
                LosUsuariosBActivos = new List<cUsuario>();
                LosListarActivos = new List<cUsuario>();

                LosUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
                LosUsuariosCBInactivos = new List<cUsuario>();
                LosUsuariosBInactivos = new List<cUsuario>();
                LosListarInactivos = new List<cUsuario>();

                LosUsuariosBActivos = LosUsuariosActivos;//POR AHORA
                LosUsuariosBInactivos = LosUsuariosInactivos;//POR AHORA
                LosUsuariosCBActivos = LosUsuariosActivos;
                LosUsuariosCBInactivos = LosUsuariosInactivos;


                grdUsuariosActivos.DataSource = dFachada.UsuarioTraerTodosActivos();
                grdUsuariosActivos.DataBind();
                grdUsuariosInactivos.DataSource = dFachada.UsuarioTraerTodosInactivos();
                grdUsuariosInactivos.DataBind();

                BFiltroPorBusqueda = false;
                BFiltroPorCheckBox = false;
            }
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            LosUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
            LosUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
            LosUsuariosCBActivos = new List<cUsuario>();
            LosUsuariosCBInactivos = new List<cUsuario>();

            if (!cbFisioterapeuta.Checked && !cbFonoaudiologo.Checked && !cbPedagogo.Checked && !cbPsicologo.Checked
                    && !cbPsicomotricista.Checked && !cbSinEspecialidad.Checked && !cbAdministrador.Checked &&
                    !cbAdministrativo.Checked && !cbEspecialista.Checked)
            {
                if (BFiltroPorBusqueda)
                {
                    LosListarActivos = LosUsuariosBActivos;
                    LosListarInactivos = LosUsuariosBInactivos;
                    BFiltroPorCheckBox = false;
                }
                else
                {
                    LosUsuariosCBActivos = LosUsuariosActivos;
                    LosUsuariosCBInactivos = LosUsuariosInactivos;
                    LosListarActivos = LosUsuariosCBActivos;
                    LosListarInactivos = LosUsuariosCBInactivos;
                    BFiltroPorCheckBox = false;
                }

            }
            else
            {
                BFiltroPorCheckBox = true;
                #region Agrego a la lstUsuariosCBActivos e Inactivos los usuarios que tengan la especialidad y tipo de usuario deseada

                if (cbFisioterapeuta.Checked || cbFonoaudiologo.Checked || cbPedagogo.Checked || cbPsicologo.Checked || cbPsicomotricista.Checked || cbSinEspecialidad.Checked)
                {
                    #region cbFisioterapeuta
                    if (this.cbFisioterapeuta.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }

                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Fisioterapia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Fonoaudiologo
                    if (this.cbFonoaudiologo.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Fonoaudiologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Fonoudiologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Pedadogo
                    if (this.cbPedagogo.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Pedadogia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Psicologo
                    if (this.cbPsicologo.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Psicologia")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                }
                            }
                        }
                    }
                    #endregion
                    #region Psicomotricista
                    if (this.cbPsicomotricista.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Psicomotricidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                }
                            }
                        }

                    }
                    #endregion
                    #region SinEspecialidad
                    if (this.cbSinEspecialidad.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                                }
                            }
                        }
                        for (int i = 0; i < LosUsuariosInactivos.Count; i++)
                        {
                            if (LosUsuariosInactivos[i].Especialidad.Nombre == "Sin especialidad")
                            {
                                if (this.cbAdministrador.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbAdministrativo.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (this.cbEspecialista.Checked)
                                {
                                    if (LosUsuariosInactivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                                    {
                                        LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
                                    }
                                }
                                if (!cbEspecialista.Checked && !cbAdministrador.Checked && !cbAdministrativo.Checked)
                                {
                                    LosUsuariosCBInactivos.Add(LosUsuariosInactivos[i]);
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
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                            {
                                LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                            }
                        }
                        for (int j = 0; j < LosUsuariosInactivos.Count; j++)
                        {
                            if (LosUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Administrador)
                            {
                                LosUsuariosCBInactivos.Add(LosUsuariosInactivos[j]);
                            }
                        }
                    }
                    if (this.cbAdministrativo.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                            {
                                LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                            }
                        }
                        for (int j = 0; j < LosUsuariosInactivos.Count; j++)
                        {
                            if (LosUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Administrativo)
                            {
                                LosUsuariosCBInactivos.Add(LosUsuariosInactivos[j]);
                            }
                        }
                    }
                    if (this.cbEspecialista.Checked)
                    {
                        for (int i = 0; i < LosUsuariosActivos.Count; i++)
                        {
                            if (LosUsuariosActivos[i].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                            {
                                LosUsuariosCBActivos.Add(LosUsuariosActivos[i]);
                            }
                        }
                        for (int j = 0; j < LosUsuariosInactivos.Count; j++)
                        {
                            if (LosUsuariosInactivos[j].Tipo == cUtilidades.TipoDeUsuario.Usuario)
                            {
                                LosUsuariosCBInactivos.Add(LosUsuariosInactivos[j]);
                            }
                        }
                    }
                }
                #endregion

                List<cUsuario> lstActivos = new List<cUsuario>();
                List<cUsuario> lstInactivos = new List<cUsuario>();

                if (BFiltroPorBusqueda)
                {
                    for (int a = 0; a < LosUsuariosBActivos.Count; a++)
                    {
                        for (int b = 0; b < LosUsuariosCBActivos.Count; b++)
                        {
                            if (LosUsuariosBActivos[a].NickName == LosUsuariosCBActivos[b].NickName)
                            {
                                lstActivos.Add(LosUsuariosBActivos[a]);
                            }
                        }
                    }
                    for (int a = 0; a < LosUsuariosBInactivos.Count; a++)
                    {
                        for (int b = 0; b < LosUsuariosCBInactivos.Count; b++)
                        {
                            if (LosUsuariosBInactivos[a].NickName == LosUsuariosCBInactivos[b].NickName)
                            {
                                lstInactivos.Add(LosUsuariosBInactivos[a]);
                            }
                        }
                    }
                    LosListarActivos = lstActivos;
                    LosListarInactivos = lstInactivos;

                }
                else
                {
                    LosListarActivos = LosUsuariosCBActivos;
                    LosListarInactivos = LosUsuariosCBInactivos;
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

            this.grdUsuariosActivos.DataSource = LosListarActivos;
            this.grdUsuariosActivos.DataBind();
            this.grdUsuariosActivos.SelectedIndex = -1;
            this.grdUsuariosInactivos.DataSource = LosListarInactivos;
            this.grdUsuariosInactivos.DataBind();
            this.grdUsuariosInactivos.SelectedIndex = -1;
            if (LosListarInactivos.Count == 0)
            {
                this.pnlUsuariosInactivos.Visible = false;
                this.lblUsuariosInactivos.Text = "No hay usuarios inactivos";
            }
            else
            {
                this.pnlUsuariosInactivos.Visible = true;
                this.lblUsuariosInactivos.Text = "Usuarios inactivos";
            }
            if (LosListarActivos.Count == 0)
            {
                this.pnlUsuariosActivos.Visible = false;
                this.lblUsuariosActivos.Text = "No hay usuarios activos";
            }
            else
            {
                this.pnlUsuariosActivos.Visible = true;
                this.lblUsuariosActivos.Text = "Usuarios activos";
            }
        }

        protected void txtBuscarBeneficiario_TextChanged(object sender, EventArgs e)
        {
            string sTexto = txtBuscarBeneficiario.Text;
            LosUsuariosActivos = dFachada.UsuarioTraerTodosActivos();
            LosUsuariosInactivos = dFachada.UsuarioTraerTodosInactivos();
            LosUsuariosBActivos = new List<cUsuario>();
            LosUsuariosBInactivos = new List<cUsuario>();

            if (sTexto == "")
            {
                LosUsuariosBActivos = LosUsuariosActivos;
                LosUsuariosBInactivos = LosUsuariosInactivos;
                BFiltroPorBusqueda = false;
                if (BFiltroPorCheckBox)
                {
                    LosListarActivos = LosUsuariosCBActivos;
                    LosListarInactivos = LosUsuariosCBInactivos;
                }
                else
                {
                    LosListarActivos = LosUsuariosBActivos;
                    LosListarInactivos = LosUsuariosBInactivos;
                }
            }
            else
            {
                BFiltroPorBusqueda = true;
                int iVal = 0;
                if (int.TryParse(txtBuscarBeneficiario.Text, out iVal))
                {
                    // es numerico
                    LosUsuariosBActivos = dFachada.UsuarioTraerTodosActivosPorCI(sTexto);
                    LosUsuariosBInactivos = dFachada.UsuarioTraerTodosInactivosPorCI(sTexto);
                }
                else
                {
                    //no lo es 
                    LosUsuariosBActivos = dFachada.UsuarioTraerTodosActivosPorNombreApellido(sTexto);
                    LosUsuariosBInactivos = dFachada.UsuarioTraerTodosInactivosPorNombreApellido(sTexto);
                }
                if (BFiltroPorCheckBox)
                {
                    LosListarActivos = LosUsuariosCBActivos;
                    LosListarInactivos = LosUsuariosCBInactivos;

                    List<cUsuario> lstActivos = new List<cUsuario>();
                    List<cUsuario> lstInactivos = new List<cUsuario>();


                    if (LosUsuariosBActivos.Count > 0 && LosListarActivos.Count > 0)
                    {
                        for (int i = 0; i < LosListarActivos.Count; i++)
                        {
                            for (int j = 0; j < LosUsuariosBActivos.Count; j++)
                            {
                                if (LosListarActivos[i].NickName == LosUsuariosBActivos[j].NickName)
                                {
                                    lstActivos.Add(LosListarActivos[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        LosListarActivos = new List<cUsuario>();
                    }

                    if (LosUsuariosBInactivos.Count > 0 && LosListarInactivos.Count > 0)
                    {
                        for (int i = 0; i < LosListarInactivos.Count; i++)
                        {
                            for (int j = 0; j < LosUsuariosBInactivos.Count; j++)
                            {
                                if (LosListarInactivos[i].NickName == LosUsuariosBInactivos[j].NickName)
                                {
                                    lstInactivos.Add(LosListarInactivos[i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        LosListarInactivos = new List<cUsuario>();
                    }
                    LosListarActivos = lstActivos;
                    LosListarInactivos = lstInactivos;
                }
                else
                {
                    LosListarActivos = LosUsuariosBActivos;
                    LosListarInactivos = LosUsuariosBInactivos;
                }



            }



            CargarGrillas();
        }



        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            #region
            BFiltroPorCheckBox = false;
            if (BFiltroPorBusqueda)
            {
                LosListarActivos = LosUsuariosBActivos;
                LosListarInactivos = LosUsuariosBInactivos;
            }
            else
            {
                LosListarActivos = LosUsuariosActivos;
                LosListarInactivos = LosUsuariosInactivos;
            }
            
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
            string sNickName = string.Format(celdaNickName.Text);
            if (vMiPerfil.U.NickName == sNickName)
            {
                this.grdUsuariosActivos.SelectedIndex = 3;
                Response.Redirect("vMiPerfil.aspx");

            }
            else
            {
                Response.Redirect("vUsuarioDetalles.aspx?nickname=" + sNickName);
            }
        }

        protected void grdUsuariosInactivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            TableCell celdaNickName = grdUsuariosInactivos.Rows[e.NewSelectedIndex].Cells[2];
            string sNickName = string.Format(celdaNickName.Text);
            Response.Redirect("vUsuarioDetalles.aspx?nickname=" + sNickName);
        }

        protected void grdUsuariosActivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[8].Visible = false; //domicilio
            e.Row.Cells[10].Visible = false; //tel
            e.Row.Cells[11].Visible = false; //email
            e.Row.Cells[12].Visible = false;//estadp
            e.Row.Cells[13].Visible = false; //tipo contrato
        }

        protected void grdUsuariosInactivos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false; //codigo
            e.Row.Cells[3].Visible = false; //contraseña
            e.Row.Cells[8].Visible = false; //domicilio
            e.Row.Cells[10].Visible = false; //tel
            e.Row.Cells[11].Visible = false; //email
            e.Row.Cells[12].Visible = false;//estadp
            e.Row.Cells[13].Visible = false; //tipo contrato

        }

        #endregion
    }
}