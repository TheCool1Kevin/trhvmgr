namespace trhvmgr.Objects
{
    /// <summary>
    /// Object that can be converted to an IDbObject
    /// </summary>
    /// <typeparam name="T">Type that implements IDbObject</typeparam>
    public interface ISerializableDbObject<T> where T : IDbObject
    {
        T GetDbObject();
    }

    /// <summary>
    /// Wrapping type that is inserted into the database.
    /// </summary>
    public interface IDbObject { }
}
