using System;

public static class UserService {
    public static Guid? Auth(string login, string pass){
        if (login == "admin" && pass == "pwd")
        {
            return new Guid("cbdf780d-d48f-4eaa-8574-dcedeb136287");
        }

        return null;
    }

    internal static bool IsUserExists(Guid guid)
    {
        return guid.ToString() == "cbdf780d-d48f-4eaa-8574-dcedeb136287";
    }
}