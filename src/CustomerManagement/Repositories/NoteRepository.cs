using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerManagement.Repositories
{

    public class NoteRepository : BaseRepository, IRepository<Note>
    {
        public Note Create(Note entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [Notes] (CustomerId,Note) " +
                                             "VALUES (@CustomerId, @Note);" +
                                             "SELECT IDENT_CURRENT('Notes') as Id;", connection);

                var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = entity.CustomerId
                };
                var textParameter = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
                {
                    Value = entity.Text
                };

                command.Parameters.Add(customerIdParameter);
                command.Parameters.Add(textParameter);

                using (var reader = command.ExecuteReader())
                {
                    int idOfCreatedEntity = 0;
                    if (reader.Read())
                    {
                        idOfCreatedEntity = int.Parse(reader["Id"].ToString() ?? string.Empty);
                    }
                    return Read(idOfCreatedEntity);
                }

            }

        }

        public Note Read(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notes WHERE NoteId = @Id", connection);

                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };

                command.Parameters.Add(idParam);

                using (var reader = command.ExecuteReader())
                {
                    
                    if (reader.Read())
                    {
                        return new Note()
                        {
                            Id = int.Parse(reader["NoteId"].ToString()),
                            CustomerId = int.Parse(reader["CustomerId"].ToString()),
                            Text = reader["Note"].ToString()
                        };
                    }
                    return null;
                }

            }
            
        }

        public List<Note> ReadAll()
        {
            var notesList = new List<Note>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notes", connection);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var note = new Note();

                        note.Id = int.Parse(reader["NoteId"].ToString());
                        note.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        note.Text = reader["Note"].ToString();

                        notesList.Add(note);
                    }
                }

            }

            return notesList;
        }

        public List<Note> ReadAll(int customerId)
        {
            var notesList = new List<Note>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Notes WHERE CustomerId=@CustomerId", connection);

                
                var idParam = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = customerId
                };

                command.Parameters.Add(idParam);

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var note = new Note();

                        note.Id = int.Parse(reader["NoteId"].ToString());
                        note.CustomerId = int.Parse(reader["CustomerId"].ToString());
                        note.Text = reader["Note"].ToString();

                        notesList.Add(note);
                    }
                }

            }

            return notesList;
        }

        public bool Update(Note entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Notes SET CustomerId=@CustomerId, Note=@Note " +
                                             " WHERE NoteId = @NoteId", connection);


                var noteIdParameter = new SqlParameter("@NoteId", SqlDbType.Int)
                {
                    Value = entity.Id
                };
                var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
                {
                    Value = entity.CustomerId
                };
                var textParameter = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
                {
                    Value = entity.Text
                };

                command.Parameters.Add(noteIdParameter);
                command.Parameters.Add(customerIdParameter);
                command.Parameters.Add(textParameter);

                if (command.ExecuteNonQuery() > 0) return true;
                else return false;
            }

        }

        public bool Delete(int entityId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Notes WHERE NoteId = @Id", connection);

                var idParameter = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = entityId
                };

                command.Parameters.Add(idParameter);

                if (command.ExecuteNonQuery() > 0) return true;
                else return false;
            }

        }

    }
}