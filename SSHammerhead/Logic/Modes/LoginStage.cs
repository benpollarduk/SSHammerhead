namespace SSHammerhead.Logic.Modes
{
    /// <summary>
    /// Provides an enumeration of login stages.
    /// </summary>
    public enum LoginStage
    {
        /// <summary>
        /// User name.
        /// </summary>
        UserName,
        /// <summary>
        /// Invalid user name.
        /// </summary>
        InvalidUserName,
        /// <summary>
        /// Password.
        /// </summary>
        Password,
        /// <summary>
        /// Invalid password.
        /// </summary>
        InvalidPassword,
        /// <summary>
        /// Start maintenance.
        /// </summary>
        StartMaintenance
    }
}
