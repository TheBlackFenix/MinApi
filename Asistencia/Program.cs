using System.Collections.Generic;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
string strcon = builder.Configuration.GetConnectionString("Asistencia");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Asistencia",()=>
{
    using (SqlConnection con = new SqlConnection(strcon))
    {
        List<Dictionary<string,string>> dic = new List<Dictionary<string, string>>();
        string query = "Exec [Ten Most Expensive Products]";
        using (SqlCommand Com = new SqlCommand(query))
        {
            Com.Connection = con;
            con.Open();
            using (SqlDataReader read = Com.ExecuteReader())
            {
                while (read.Read())
                {
                    var a1 = read.GetName(0);
                    var a11 = read.GetName(1);
                    var a2 = read.GetColumnSchema();
                    var a3 = read.HasRows;
                    var a4 = read.VisibleFieldCount;
                    var temp = new Dictionary<string,string>();
                    for(var a = 0; a <= read.FieldCount-1;a++)
                    {
                        temp.Add(read.GetName(a).ToString(),read.GetValue(a).ToString());
                    }
                    dic.Add(temp);
                }
            }
        }
        return dic;
    }
   
}
);


app.Run();
