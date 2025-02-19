namespace Business.Helpers;

public class ProjectNumberGenerator
{
    //Genereras av ChatGpt 
    public static string GenerateProjectNumber()
    {
        return $"P-{Guid.NewGuid().ToString().Substring(0, 3).ToUpper()}"; // skapar en unik identifierare, och tar de första 3 tecknen från den slumpmässiga GUID-strängen.
    }

}
