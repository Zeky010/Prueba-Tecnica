using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System;
using System.Threading.Tasks;
namespace ApiPosts.Models
{
    public class DataAccessLayer
    {
        private readonly string _connectionString;

        public DataAccessLayer()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public async Task<List<Post>> GetPosts()
        {
            List<Post> PostList = new List<Post>();

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM POSTS", connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Post post = new Post
                            {
                                id = (int)reader["id"],
                                nombre = (string)reader["nombre"],
                                descripcion = (string)reader["descripcion"]
                            };
                            PostList.Add(post);
                        }
                    }
                }
            }

            return PostList;
        }

        public async Task<Post> GetPostById(int id)
        {
            Post post = null;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM POSTS WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {                           
                            if (reader.Read())
                            {
                                post = new Post
                                {
                                    id = (int)reader["Id"],
                                    nombre = (string)reader["nombre"],
                                    descripcion = (string)reader["descripcion"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return post;
        }

        public async Task<Post> GetPostByName(string nombre)
        {
            Post post = null;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM POSTS WHERE NOMBRE = @nombre", connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                post = new Post
                                {
                                    id = (int)reader["Id"],
                                    nombre = (string)reader["nombre"],
                                    descripcion = (string)reader["descripcion"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return post;
        }

        public async Task<Post> InsertPost(Post post)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO POSTS (NOMBRE, DESCRIPCION) VALUES (@nom, @desc)", connection))
                    {
                        command.Parameters.AddWithValue("@nom", post.nombre);
                        command.Parameters.AddWithValue("@desc", post.descripcion);
                        if (await command.ExecuteNonQueryAsync() > 0) 
                        {
                            //return await GetPostByName(post.nombre);
                            return post;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePost(Post post)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("UPDATE POSTS SET nombre = @nom, descripcion = @desc WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@nom", post.nombre);
                        command.Parameters.AddWithValue("@desc", post.descripcion);

                        command.Parameters.AddWithValue("@Id", post.id);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true ;
        }
        public async Task<Post> DeletePost(int id)
        {
            Post post = null;
            try
            {
                post = await GetPostById(id);
                if (post is null)
                {
                    return post;
                }
                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM POSTS WHERE id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        if (await command.ExecuteNonQueryAsync() > 0) return post;
                        else return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}