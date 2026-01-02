using System;
using System.Collections.Generic;
using System.Text;

public class UserService
{
    private Dictionary<string, string> users = new();

    public void Register(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username cannot be empty");
        if (password == null || password.Length < 6)
            throw new ArgumentException("Password too short");
        users[username] = password;
    }

    public bool Login(string username, string password)
    {
        if (!users.ContainsKey(username))
            throw new Exception("User not registered");
        return users[username] == password;
    }

    public void ResetPassword(string username, string newPassword)
    {
        if (!users.ContainsKey(username))
            throw new Exception("User not found");
        users[username] = newPassword;
    }
}


