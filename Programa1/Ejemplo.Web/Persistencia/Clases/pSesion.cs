using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Clases;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia.Clases
{
    public class pSesion : pPersistencia
    {
        public static bool Agregar(cSesion parSesion)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_Agregar", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionTipo", parSesion.TipoSesion));
                cmd.Parameters.Add(new SqlParameter("@SesionFecha", DateTime.Parse(parSesion.Fecha)));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));
                cmd.Parameters.Add(new SqlParameter("@SesionCentro", parSesion.Centro));

                int iRtn = cmd.ExecuteNonQuery();

                if (iRtn <= 0)
                {
                    bRetorno = false;
                }

                if (bRetorno)
                {
                    SqlCommand cmd1 = new SqlCommand("Sesiones_TraerUltimoId", vConn);
                    cmd1.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader oReader = cmd1.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            parSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        }
                    }

                    for (int i = 0; i < parSesion.lstBeneficiarios.Count; i++)
                    {
                        SqlCommand cmd2 = new SqlCommand("BeneficiariosSesiones_Agregar", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@BeneficiarioId", parSesion.lstBeneficiarios[i].Beneficiario.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                        cmd2.Parameters.Add(new SqlParameter("@Estado", parSesion.lstBeneficiarios[i].Estado));

                        //ARREGLAR CUANDO SE PONGA EN PRESENTACION PARA ELEGIR EL PLAN
                        cmd2.Parameters.Add(new SqlParameter("@PlanId", parSesion.lstBeneficiarios[i].Plan.Codigo));
                        cmd2.ExecuteNonQuery();
                    }
                    for (int i = 0; i < parSesion.lstUsuarios.Count; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("UsuariosSesiones_Agregar", vConn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add(new SqlParameter("@UsuarioId", parSesion.lstUsuarios[i].Codigo));
                        cmd3.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                        cmd3.ExecuteNonQuery();
                    }
                }

                if (vConn.State == ConnectionState.Open)
                {
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }
        public static List<cSesion> TraerPasaronDelDia()
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerPasaronDelDia", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] ss = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = ss[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int b = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (b == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (b == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (b == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (b == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;

                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static List<cSesion> TraerProximasDelDiaPorEspecialista(cUsuario parUsuario)
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerProximasDelDiaPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iAa = int.Parse(oReader["SesionCentro"].ToString());
                        if (iAa == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iAa == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;

                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cSesion> TraerPasaronDelDiaPorEspecialista(cUsuario parUsuario)
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerPasaronDelDiaPorEspecialista", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idEspecialista", parUsuario.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;

                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static cSesion TraerEspecifico(cSesion parSesion)
        {
            cSesion unRetorno = null;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerEspecifico", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno = new cSesion();
                        unRetorno.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unRetorno.Comentario = oReader["SesionComentario"].ToString();
                        unRetorno.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unRetorno.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unRetorno.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unRetorno.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unRetorno.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unRetorno.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unRetorno.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unRetorno.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unRetorno.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unRetorno.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unRetorno.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unRetorno.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unRetorno.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unRetorno.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unRetorno.lstUsuarios = new List<cUsuario>();
                        unRetorno.lstUsuarios = lstUsuarios;
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
        public static bool MarcarAsitencias(cSesion parSesion)
        {
            bool bRetorno = true;

            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                foreach (cBeneficiarioSesion unBen in parSesion.lstBeneficiarios)
                {
                    SqlCommand cmd = new SqlCommand("BeneficiariosSesiones_MarcarAsistencia", vConn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                    cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", unBen.Beneficiario.Codigo));
                    int iEstado;
                    switch (unBen.Estado)
                    {
                        case cUtilidades.EstadoSesion.Asistio:
                            iEstado = 0;
                            break;
                        case cUtilidades.EstadoSesion.NoAsistio:
                            iEstado = 1;
                            break;
                        case cUtilidades.EstadoSesion.Reprogramada:
                            iEstado = 2;
                            break;
                        default:
                            iEstado = 3;
                            break;

                    }
                    cmd.Parameters.Add(new SqlParameter("@BeneficiarioSesionesEstado", iEstado));

                    int iRtn = cmd.ExecuteNonQuery();
                }
                SqlCommand cmd1 = new SqlCommand("Sesiones_AgregarComentario", vConn);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new SqlParameter("@SesionId", parSesion.Codigo));
                cmd1.Parameters.Add(new SqlParameter("@SesionComentario", parSesion.Comentario));

                int iRtn1 = cmd1.ExecuteNonQuery();
                if (iRtn1 <= 0)
                {
                    bRetorno = false;
                }

                if (vConn.State == ConnectionState.Open)
                {
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }
        public static List<cUsuario> VerificarFechaYHorarioUsuario(cSesion parSesion) //Devuelve lista de usuarios que no están disponibles
        {
            List<cUsuario> lstRetorno = new List<cUsuario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSesiones_VerificarFechaYHorario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionFecha", parSesion.Fecha));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["UsuarioId"].ToString());
                        for (int i = 0; i < parSesion.lstUsuarios.Count; i++)
                        {
                            if (parSesion.lstUsuarios[i].Codigo == iCodigo)
                            {
                                lstRetorno.Add(parSesion.lstUsuarios[i]);
                            }
                        }

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static List<cBeneficiario> VerificarFechaYHorarioBeneficiario(cSesion parSesion) //Devuelve lista de beneficiarios que no están disponibles
        {
            List<cBeneficiario> lstRetorno = new List<cBeneficiario>();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosSesiones_VerificarFechaYHorario", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SesionFecha", parSesion.Fecha));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraInicio", parSesion.HoraInicio));
                cmd.Parameters.Add(new SqlParameter("@SesionHoraFin", parSesion.HoraFin));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        int iCodigo = int.Parse(oReader["BeneficiarioId"].ToString());
                        for (int i = 0; i < parSesion.lstBeneficiarios.Count; i++)
                        {
                            if (parSesion.lstBeneficiarios[i].Beneficiario.Codigo == iCodigo)
                            {
                                lstRetorno.Add(parSesion.lstBeneficiarios[i].Beneficiario);
                            }
                        }

                    }
                }
                vConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static bool AgregarObservacion(cUsuarioSesion parUS)
        {
            bool bRetorno = true;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSesiones_AgregarObservacion", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUS.Usuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@SesionId", parUS.Sesion.Codigo));
                cmd.Parameters.Add(new SqlParameter("@observacion", parUS.Observacion));
                int iRtn = cmd.ExecuteNonQuery();
                if (iRtn <= 0)
                {
                    bRetorno = false;
                }
                if (vConn.State == ConnectionState.Open)
                {
                    vConn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bRetorno;
        }
        public static List<cSesion> TraerTodasPorEspecialistaConFiltros(string parConsulta)
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand(parConsulta, vConn);
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;

                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
        public static cUsuarioSesion TraerObservacionPorUsuarioYSesion(cUsuarioSesion parUsuarioSesion)
        {
            cUsuarioSesion unRetorno = new cUsuarioSesion();
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("UsuariosSesiones_TraerObservacionPorUsuarioYSesion", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SesionId", parUsuarioSesion.Sesion.Codigo));
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuarioSesion.Usuario.Codigo));
                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unRetorno.Usuario = parUsuarioSesion.Usuario;
                        unRetorno.Sesion = parUsuarioSesion.Sesion;
                        unRetorno.Observacion = oReader["UsuariosSesionesObservacion"].ToString();
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRetorno;
        }
        public static List<DateTime> TraerMaximaFechaYMinimaFecha()
        {
            List<DateTime> lstRetorno = new List<DateTime>();
            DateTime dFecha;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_MaxFechaYMinFecha", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        dFecha = new DateTime();
                        if (oReader["maximo"] != DBNull.Value)
                        {
                            dFecha = DateTime.Parse(oReader["maximo"].ToString());
                            lstRetorno.Add(dFecha);
                        }
                        dFecha = new DateTime();
                        if (oReader["minimo"] != DBNull.Value)
                        {
                            dFecha = DateTime.Parse(oReader["minimo"].ToString());
                            lstRetorno.Add(dFecha);
                        }
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<string> TraerAsistenciasDeBeneficiarioPorMes(cBeneficiario parBeneficiario, string parAno, string parMes)
        {
            List<string> lstRetorno = new List<string>();
            for (int i = 1; i < 32; i++)
            {
                lstRetorno.Add("");
            }
            string sEstado;
            int iDia;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("BeneficiariosSesiones_AsistenciasPorMes", vConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@BeneficiarioId", parBeneficiario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@Ano", parAno));
                cmd.Parameters.Add(new SqlParameter("@Mes", parMes));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        iDia = DateTime.Parse(oReader["SesionFecha"].ToString()).Day - 1;
                        switch (int.Parse(oReader["BeneficiariosSesionesEstado"].ToString()))
                        {
                            case 0:
                                sEstado = "1";
                                break;
                            case 1:
                                sEstado = "X";
                                break;
                            case 2:
                                sEstado = "R";
                                break;
                            default:
                                sEstado = "NO_AGREGAR";
                                break;

                        }
                        if (sEstado != "NO_AGREGAR")
                        {
                            if (lstRetorno[iDia] == null || lstRetorno[iDia] == string.Empty)
                            {
                                lstRetorno[iDia] = sEstado;
                            }
                            else
                            {
                                switch (lstRetorno[iDia])
                                {
                                    case "X":
                                        if (sEstado != "X" && sEstado != "R")
                                        {
                                            lstRetorno[iDia] = sEstado;
                                        }
                                        break;
                                    case "R":
                                        if (sEstado != "R")
                                        {
                                            lstRetorno[iDia] = sEstado;
                                        }
                                        break;
                                    default:
                                        if (sEstado != "X" && sEstado != "R")
                                            lstRetorno[iDia] = (int.Parse(lstRetorno[iDia]) + int.Parse(sEstado)).ToString();
                                        break;
                                }
                            }
                        }
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cSesion> TraerPorRango(DateTime parFechaInicial, DateTime parFechaFinal, cUsuario parUsuario)
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerPorRango", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UsuarioId", parUsuario.Codigo));
                cmd.Parameters.Add(new SqlParameter("@fechaIni", parFechaInicial));
                cmd.Parameters.Add(new SqlParameter("@fechaFin", parFechaFinal));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                unBeneficiarioSesion.Plan.Tipo = oReader1["PlanTipo"].ToString();

                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;
                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }

        public static List<cSesion> TraerTodasPorFecha(DateTime parFecha, int parCentro)
        {
            List<cSesion> lstRetorno = new List<cSesion>();
            cSesion unaSesion;
            try
            {
                var vConn = new SqlConnection(CadenaDeConexion);
                vConn.Open();

                SqlCommand cmd = new SqlCommand("Sesiones_TraerTodasPorDia", vConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@fecha", parFecha));
                cmd.Parameters.Add(new SqlParameter("@centro", parCentro));

                using (SqlDataReader oReader = cmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        unaSesion = new cSesion();
                        unaSesion.Codigo = int.Parse(oReader["SesionId"].ToString());
                        unaSesion.Comentario = oReader["SesionComentario"].ToString();
                        unaSesion.Fecha = DateTime.Parse(oReader["SesionFecha"].ToString()).ToShortDateString();
                        unaSesion.HoraInicio = DateTime.Parse(oReader["SesionHoraInicio"].ToString()).ToShortTimeString();
                        unaSesion.HoraFin = DateTime.Parse(oReader["SesionHoraFin"].ToString()).ToShortTimeString();
                        int i = int.Parse(oReader["SesionTipo"].ToString());
                        if (i == 0) unaSesion.TipoSesion = cUtilidades.TipoSesion.Individual;
                        if (i == 1) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo2;
                        if (i == 2) unaSesion.TipoSesion = cUtilidades.TipoSesion.Grupo3;
                        if (i == 3) unaSesion.TipoSesion = cUtilidades.TipoSesion.Taller;
                        if (i == 4) unaSesion.TipoSesion = cUtilidades.TipoSesion.PROES;
                        int iA = int.Parse(oReader["SesionCentro"].ToString());
                        if (iA == 0) unaSesion.Centro = cUtilidades.Centro.JuanLacaze;
                        if (iA == 1) unaSesion.Centro = cUtilidades.Centro.NuevaHelvecia;

                        List<cBeneficiarioSesion> lstBeneficiariosSesion = new List<cBeneficiarioSesion>();
                        cBeneficiarioSesion unBeneficiarioSesion;
                        SqlCommand cmd1 = new SqlCommand("Beneficiarios_TraerPorSesion", vConn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader1 = cmd1.ExecuteReader())
                        {
                            while (oReader1.Read())
                            {
                                unBeneficiarioSesion = new cBeneficiarioSesion();
                                unBeneficiarioSesion.Beneficiario = new cBeneficiario();
                                unBeneficiarioSesion.Beneficiario.Codigo = int.Parse(oReader1["BeneficiarioId"].ToString());
                                unBeneficiarioSesion.Beneficiario.Nombres = oReader1["BeneficiarioNombres"].ToString();
                                unBeneficiarioSesion.Beneficiario.Apellidos = oReader1["BeneficiarioApellidos"].ToString();
                                unBeneficiarioSesion.Beneficiario.CI = int.Parse(oReader1["BeneficiarioCI"].ToString());
                                unBeneficiarioSesion.Beneficiario.Sexo = oReader1["BeneficiarioSexo"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono1 = oReader1["BeneficiarioTelefono1"].ToString();
                                unBeneficiarioSesion.Beneficiario.Telefono2 = oReader1["BeneficiarioTelefono2"].ToString();
                                unBeneficiarioSesion.Beneficiario.Domicilio = oReader1["BeneficiarioDomicilio"].ToString();
                                unBeneficiarioSesion.Beneficiario.Email = oReader1["BeneficiarioEmail"].ToString();
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = oReader1["BeneficiarioFechaNacimiento"].ToString();
                                string[] aSs = unBeneficiarioSesion.Beneficiario.FechaNacimiento.Split(' ');
                                unBeneficiarioSesion.Beneficiario.FechaNacimiento = aSs[0];
                                unBeneficiarioSesion.Beneficiario.Atributario = oReader1["BeneficiarioAtributario"].ToString();
                                unBeneficiarioSesion.Beneficiario.MotivoConsulta = oReader1["BeneficiarioMotivoConsulta"].ToString();
                                unBeneficiarioSesion.Beneficiario.Escolaridad = oReader1["BeneficiarioEscolaridad"].ToString();
                                unBeneficiarioSesion.Beneficiario.Derivador = oReader1["BeneficiarioDerivador"].ToString();
                                unBeneficiarioSesion.Beneficiario.Estado = bool.Parse(oReader1["BeneficiarioEstado"].ToString());
                                unBeneficiarioSesion.Plan = new cPlan();
                                unBeneficiarioSesion.Plan.Codigo = int.Parse(oReader1["PlanId"].ToString());
                                unBeneficiarioSesion.Plan.Tipo = oReader1["PlanTipo"].ToString();

                                int iB = int.Parse(oReader1["BeneficiariosSesionesEstado"].ToString());
                                if (iB == 0) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Asistio;
                                if (iB == 1) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.NoAsistio;
                                if (iB == 2) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.Reprogramada;
                                if (iB == 3) unBeneficiarioSesion.Estado = cUtilidades.EstadoSesion.SinEstado;
                                lstBeneficiariosSesion.Add(unBeneficiarioSesion);
                            }
                        }
                        unaSesion.lstBeneficiarios = new List<cBeneficiarioSesion>();
                        unaSesion.lstBeneficiarios = lstBeneficiariosSesion;



                        List<cUsuario> lstUsuarios = new List<cUsuario>();
                        cUsuario unUsuario;
                        SqlCommand cmd2 = new SqlCommand("Usuario_TraerTodosPorSesion", vConn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add(new SqlParameter("@idSesion", unaSesion.Codigo));

                        using (SqlDataReader oReader2 = cmd2.ExecuteReader())
                        {
                            while (oReader2.Read())
                            {
                                unUsuario = new cUsuario();
                                unUsuario.Codigo = int.Parse(oReader2["UsuarioId"].ToString());
                                unUsuario.NickName = oReader2["UsuarioNickName"].ToString();
                                unUsuario.Nombres = oReader2["UsuarioNombres"].ToString();
                                unUsuario.Apellidos = oReader2["UsuarioApellidos"].ToString();
                                unUsuario.CI = int.Parse(oReader2["UsuarioCI"].ToString());
                                int iC = int.Parse(oReader2["UsuarioTipo"].ToString());
                                if (iC == 0)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrador;
                                }
                                if (iC == 1)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Administrativo;
                                }
                                if (iC == 2)
                                {
                                    unUsuario.Tipo = cUtilidades.TipoDeUsuario.Usuario;
                                }
                                unUsuario.Domicilio = oReader2["UsuarioDomicilio"].ToString();
                                if (oReader2["UsuarioFechaNacimiento"] != DBNull.Value)
                                {
                                    unUsuario.FechaNacimiento = DateTime.Parse(oReader2["UsuarioFechaNacimiento"].ToString()).ToShortDateString();
                                }
                                unUsuario.Telefono = oReader2["UsuarioTelefono"].ToString();
                                unUsuario.Estado = bool.Parse(oReader2["UsuarioEstado"].ToString());
                                unUsuario.Email = oReader2["UsuarioEmail"].ToString();
                                string sD = oReader2["UsuarioTipoContrato"].ToString();
                                if (sD == "S")
                                {
                                    unUsuario.TipoContrato = "Socio";
                                }
                                if (sD == "C")
                                {
                                    unUsuario.TipoContrato = "Contratado";
                                }
                                if (sD == "E")
                                {
                                    unUsuario.TipoContrato = "Empleado";
                                }
                                unUsuario.Especialidad = new cEspecialidad();
                                unUsuario.Especialidad.Codigo = int.Parse(oReader2["EspecialidadId"].ToString());
                                unUsuario.Especialidad.Nombre = oReader2["EspecialidadNombre"].ToString();
                                lstUsuarios.Add(unUsuario);
                            }
                        }
                        unaSesion.lstUsuarios = new List<cUsuario>();
                        unaSesion.lstUsuarios = lstUsuarios;
                        lstRetorno.Add(unaSesion);
                    }
                    vConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstRetorno;
        }
    }
}
