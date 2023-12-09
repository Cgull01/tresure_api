namespace tresure_api.Data
{
    public static class ErrorMessages
    {
        public static Dictionary<int, string> Messages = new Dictionary<int, string>
    {
        { 400, "Request was not understood due to malformed data." },

        { 401, "Requester is unauthorized." },

        { 403, "Access to resource is restricted." },

        { 404, "Resource not found." },

        { 409, "Resource already exists." }
    };
    }
}
