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
namespace MonitoreoAPIv1.Controllers.Login
{
    public class LoginController : ApiController
    {
        public void Post(string Usuariox, string Pass)
        {
            try
            {
                DataBaseFunction db = new DataBaseFunction();
                db.agregarParametro("@Usuario", SqlDbType.VarChar, Usuariox);
                db.agregarParametro("@Password", SqlDbType.VarChar, Pass);
                SqlDataReader reader = db.consultaReader("Softv_UsuarioGetusuarioByUserAndPass");
                UsuarioM usuario = db.MapDataToEntityCollection<UsuarioM>(reader).FirstOrDefault();
                reader.Close();

                //Nos traemos los menus del usuario
                DataBaseFunction db2 = new DataBaseFunction();
                db2.agregarParametro("@IdRoll", SqlDbType.Int, usuario.IdRol);
                SqlDataReader reader2 = db2.consultaReader("GetUserMenu");
                List<Menu> lmenu = db2.MapDataToEntityCollection<Menu>(reader2).ToList();

                List<Menu> menusuario = new List<Menu>();
                List<Menu> menusprincipales = new List<Menu>();
                menusprincipales = lmenu.Where(xx => xx.ParentId == 0).ToList();
                menusprincipales.ForEach(y =>
                {
                    menusuario = lmenu.Where(p => p.ParentId.Value == y.IdModule).ToList();
                    y.MenuChild = menusuario;

                    y.MenuChild.ForEach(t =>
                    {
                        t.MenuChild = lmenu.Where(n => n.ParentId.Value == t.IdModule).ToList();
                    });
                });

                usuario.Menu = menusprincipales;
            }
            catch(Exception ex)
            {

            }
            using (SqlConnection connection = new SqlConnection(""))
            {

                SqlCommand comandoSql = new SqlCommand("Softv_UsuarioGetusuarioByUserAndPass", connection);
                UsuarioM entity_Usuario = null;
                comandoSql.CreateParameter(comandoSql, "@Usuario", Usuariox);
                AssingParameter(comandoSql, "@Password", Pass);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_Usuario = GetUsuarioFromReader(rd);

                    rd.Close();
                    List<Menu> lmenu = new List<Menu>();
                    List<Menu> menusuario = new List<Menu>();
                    List<Menu> menusprincipales = new List<Menu>();
                    SqlCommand comandoSql2 = CreateCommand("GetUserMenu", connection);
                    AssingParameter(comandoSql2, "@Rol", entity_Usuario.IdRol);
                    rd = ExecuteReader(comandoSql2);
                    while (rd.Read())
                    {
                        Menu m = new Menu();
                        m.IdModule = Int32.Parse(rd[0].ToString());
                        m.OptAdd = bool.Parse(rd[2].ToString());
                        m.OptDelete = bool.Parse(rd[3].ToString());
                        m.OptSelect = bool.Parse(rd[4].ToString());
                        m.OptUpdate = bool.Parse(rd[5].ToString());
                        m.Title = rd[6].ToString();
                        m.Class = rd[7].ToString();
                        try
                        {
                            m.ParentId = Int32.Parse(rd[8].ToString());
                        }
                        catch
                        {
                            m.ParentId = 0;
                        }

                        m.Icon = rd[9].ToString();
                        try
                        {
                            m.SortOrder = Int32.Parse(rd[10].ToString());
                        }
                        catch
                        {
                        }

                        lmenu.Add(m);

                    }
                    rd.Close();
                    menusprincipales = lmenu.Where(xx => xx.ParentId == 0).ToList();
                    menusprincipales.ForEach(y =>
                    {
                        menusuario = lmenu.Where(p => p.ParentId.Value == y.IdModule).ToList();
                        y.MenuChild = menusuario;

                        y.MenuChild.ForEach(t =>
                        {
                            t.MenuChild = lmenu.Where(n => n.ParentId.Value == t.IdModule).ToList();
                        });
                    });

                    entity_Usuario.Menu = menusprincipales;


                }
                catch (Exception ex)
                {
                    throw new Exception("Error Get usuario By User And Pass " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_Usuario;
            }
        }
    }
}
