using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MonitoreoAPIv1.Models;
using MonitoreoAPIv1.Controllers.Helpers;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Text;

namespace MonitoreoAPIv1.Controllers.Login
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Post(string Usuariox, string Pass)
        {
            try
            {
                //Emcabezado
                HttpRequestMessage re = new HttpRequestMessage();
                var headers = re.Headers;
                string credentials = "";
                if (headers.Contains("Authorization"))
                {
                    string encodedUnamePwd = headers.GetValues("Authorization").First();
                    byte[] decodedBytes = null;
                    decodedBytes = Convert.FromBase64String(encodedUnamePwd);
                    credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);
                }                

                // Validate User and Password
                string[] authParts = credentials.Split(':');
                //Fin encabezado

                DataBaseFunction db = new DataBaseFunction();
                db.agregarParametro("@Usuario", SqlDbType.VarChar, Usuariox);
                db.agregarParametro("@Password", SqlDbType.VarChar, Pass);
                SqlDataReader reader = db.consultaReader("Softv_UsuarioGetusuarioByUserAndPass");
                UsuarioM usuario = db.MapDataToEntityCollection<UsuarioM>(reader).FirstOrDefault();
                reader.Close();

                //Nos traemos los menus del usuario
                DataBaseFunction db2 = new DataBaseFunction();
                db2.agregarParametro("@Rol", SqlDbType.Int, usuario.IdRol);
                SqlDataReader reader2 = db2.consultaReader("GetUserMenu");
                List<Menu> lmenu = db2.MapDataToEntityCollection<Menu>(reader2).ToList();

                List<Menu> menusuario = new List<Menu>();
                List<Menu> menusprincipales = new List<Menu>();
                menusprincipales = lmenu.Where(xx => xx.ParentId == 0).ToList();
                menusprincipales.ForEach(y =>
                {
                    menusuario = lmenu.Where(p => p.ParentId == y.IdModule).ToList();
                    y.MenuChild = menusuario;

                    y.MenuChild.ForEach(t =>
                    {
                        t.MenuChild = lmenu.Where(n => n.ParentId == t.IdModule).ToList();
                    });
                });

                usuario.Menu = menusprincipales;

                if (usuario != null)
                {
                    List<Session> lstSessions = GetSession();
                    if (lstSessions.Any(x => x.IdUsuario == usuario.IdUsuario))
                    {
                        foreach (Session i in lstSessions.Where(x => x.IdUsuario == usuario.IdUsuario))
                        {
                            DeleteSession(i.IdSession);
                        }
                    }
                    byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                    byte[] key = Guid.NewGuid().ToByteArray();
                    string token = Convert.ToBase64String(time.Concat(key).ToArray());
                    AddSession(new Session() { IdUsuario = usuario.IdUsuario, Token = token });
                    usuario.Token = token;
                    usuario.Password = "";

                    var R = usuario.IdRol;
                    var U = usuario.IdUsuario;

                    List<UsuarioM> usua = GetUsuario();
                    var usua2 = usua.Where(x => x.IdUsuario == U);
                    var total = usua2.Count();

                    List<Permiso> per = GetPermiso();
                    List<Permiso> per2 = per.Where(x => x.IdRol == R).ToList();

                    usuario.permiso2 = per2;

                    string jsonText = JsonConvert.SerializeObject(usuario);
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(jsonText, Encoding.UTF8, "appliction/json")
                    };
                }
                else
                {
                    string jsonText = JsonConvert.SerializeObject(new UsuarioM());
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(jsonText, Encoding.UTF8, "appliction/json")
                    };
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
            }
        }

        public List<Session> GetSession()
        {
            List<Session> SessionList = new List<Session>();
            DataBaseFunction db = new DataBaseFunction();
            SqlDataReader reader = db.consultaReader("Softv_SessionGet");
            SessionList = db.MapDataToEntityCollection<Session>(reader).ToList();
            reader.Close();
            return SessionList;
        }

        public int DeleteSession(long? IdSession)
        {
            int result = 0;
            DataBaseFunction db = new DataBaseFunction();
            db.agregarParametro("IdSession", SqlDbType.BigInt, IdSession);
            db.consultaSinRetorno("Softv_SessionDelete");
            return result;
        }

        public int AddSession(Session entity_Session)
        {
            int result = 0;
            DataBaseFunction db = new DataBaseFunction();
            db.agregarParametro("@IdSession", SqlDbType.BigInt, ParameterDirection.Output);
            db.agregarParametro("@IdUsuario", SqlDbType.BigInt, entity_Session.IdUsuario);
            db.agregarParametro("@Token", SqlDbType.VarChar, entity_Session.Token);
            db.consultaOutput("Softv_SessionAdd");
            result = int.Parse(db.diccionarioOutput["@IdSession"].ToString());
            return result;
        }

        public List<UsuarioM> GetUsuario()
        {
            List<UsuarioM> UsuarioList = new List<UsuarioM>();
            DataBaseFunction db = new DataBaseFunction();
            SqlDataReader reader = db.consultaReader("Softv_UsuarioGet");
            UsuarioList = db.MapDataToEntityCollection<UsuarioM>(reader).ToList();
            return UsuarioList;
        }

        public List<Permiso> GetPermiso()
        {
            List<Permiso> PermisoList = new List<Permiso>();
            DataBaseFunction db = new DataBaseFunction();
            SqlDataReader reader = db.consultaReader("Softv_PermisoGet");
            PermisoList = db.MapDataToEntityCollection<Permiso>(reader).ToList();
            return PermisoList;
        }
    }
}
