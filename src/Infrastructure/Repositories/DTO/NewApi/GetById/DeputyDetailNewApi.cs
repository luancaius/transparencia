using Newtonsoft.Json;

namespace Repositories.DTO.NewApi.GetById;

public class DeputyDetailNewApi
{
    public string Id { get; set; }
    public string Uri { get; set; }
    public string Nome { get; set; }
    public string SiglaPartido { get; set; }
    public string UriPartido { get; set; }
    public string SiglaUf { get; set; }
    public int IdLegislatura { get; set; }
    public string UrlFoto { get; set; }
    public string Email { get; set; }
    
    public DeputyDetailNewApi(string deputyDetailString)
    {
        try
        {
            var deputyDetail = JsonConvert.DeserializeObject<DeputyDetailNewApi>(deputyDetailString);
            Id = deputyDetail.Id;
            Uri = deputyDetail.Uri;
            Nome = deputyDetail.Nome;
            SiglaPartido = deputyDetail.SiglaPartido;
            UriPartido = deputyDetail.UriPartido;
            SiglaUf = deputyDetail.SiglaUf;
            IdLegislatura = deputyDetail.IdLegislatura;
            UrlFoto = deputyDetail.UrlFoto;
            Email = deputyDetail.Email;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}