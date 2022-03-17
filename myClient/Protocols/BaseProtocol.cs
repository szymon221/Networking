using System.IO;
namespace location.Protocols
{
    public abstract class BaseProtocol
    {
        /// <summary>
        /// Sets 2 internal vairables for user and location. Needs to be used
        /// with all update requests
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Location"></param>
        public abstract void SetVariables(string User, string Location);
        /// <summary>
        /// Sets a internal user variable. Needs to be used for all protocls
        /// </summary>
        /// <param name="User"></param>
        public abstract void SetVariables(string User);
        /// <summary>
        /// Sets a internal hostname Variable. Only needs to be used for http/1.1
        /// </summary>
        /// <param name="HostName"></param>
        public abstract void SetHostName(string HostName);
        /// <summary>
        /// Writes the query request to the tcp stream
        /// </summary>
        /// <param name="sw"></param>
        public abstract void Query(StreamWriter sw);
        /// <summary>
        /// Writes the Update request to the tcp stream
        /// </summary>
        /// <param name="sw"></param>
        public abstract void Update(StreamWriter sw);
        /// <summary>
        /// Takes the server response and return body
        /// If parsing query repsonse should not be supplied
        /// </summary>
        /// <param name="response"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public abstract string Body(string response, string? location = null);
        /// <summary>
        /// Checks if the response is OK.200
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public abstract bool OK(string response);
        /// <summary>
        /// Checks if the reponse is 404 Error
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public abstract bool Error(string response);

    }
}
