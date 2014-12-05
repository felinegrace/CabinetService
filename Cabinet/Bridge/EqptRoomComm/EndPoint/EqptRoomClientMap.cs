using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class EqptRoomClientMap
    {
        private Dictionary<Guid, Guid> dictKeyByEqptRoomGuid { get; set; }
        private Dictionary<Guid, Guid> dictKeyBySessionGuid { get; set; }
        private object dictLocker { get; set; }
        public EqptRoomClientMap()
        {
            dictKeyByEqptRoomGuid = new Dictionary<Guid, Guid>();
            dictKeyBySessionGuid = new Dictionary<Guid, Guid>();
            dictLocker = new object();
        }

        public void put(Guid eqptRoomGuid, Guid sessionGuid)
        {
            if (eqptRoomGuid == Guid.Empty || sessionGuid == Guid.Empty)
            {
                throw new EqptRoomCommException("eqpt room guid or session guid put to map cannot be empty.");
            }
            lock (dictLocker)
            {
                if (dictKeyByEqptRoomGuid.ContainsKey(eqptRoomGuid))
                {
                    Guid existedSessionGuid = dictKeyByEqptRoomGuid[eqptRoomGuid];
                    if (dictKeyBySessionGuid.ContainsKey(existedSessionGuid))
                    {
                        dictKeyBySessionGuid.Remove(existedSessionGuid);
                    }
                    dictKeyByEqptRoomGuid[eqptRoomGuid] = sessionGuid;
                }
                else
                {
                    dictKeyByEqptRoomGuid.Add(eqptRoomGuid, sessionGuid);
                }

                if (dictKeyBySessionGuid.ContainsKey(sessionGuid))
                {
                    Guid existedEqptRoomGuid = dictKeyBySessionGuid[sessionGuid];
                    if (dictKeyByEqptRoomGuid.ContainsKey(existedEqptRoomGuid))
                    {
                        dictKeyByEqptRoomGuid.Remove(existedEqptRoomGuid);
                    }
                    dictKeyBySessionGuid[sessionGuid] = eqptRoomGuid;
                }
                else
                {
                    dictKeyBySessionGuid.Add(sessionGuid, eqptRoomGuid);
                }
                
            }
            Logger.debug("EqptRoomHub: eqpt room session {0} by key {1} has put into EqptRoomMap.",
                eqptRoomGuid, sessionGuid);
        }

        public Guid searchSessionGuid(Guid eqptRoomGuid)
        {
            Guid sessionGuid = Guid.Empty;
            if (dictKeyByEqptRoomGuid.TryGetValue(eqptRoomGuid, out sessionGuid) == false)
            {
                Logger.debug("EqptRoomHub: no session for eqpt room guid {0}.", eqptRoomGuid);
            }
            return sessionGuid;
        }

        public Guid searchEqptRoomGuid(Guid sessionGuid)
        {
            Guid eqptRoomGuid = Guid.Empty;
            if (dictKeyBySessionGuid.TryGetValue(sessionGuid, out eqptRoomGuid) == false)
            {
                Logger.debug("EqptRoomHub: no eqpt room guid for session guid {0}.", sessionGuid);
            }
            return eqptRoomGuid;
        }

        public void removeByEqptRoomGuid(Guid eqptRoomGuid)
        {
            try
            {
                lock (dictLocker)
                {
                    Guid sessionGuid = dictKeyByEqptRoomGuid[eqptRoomGuid];
                    dictKeyByEqptRoomGuid.Remove(eqptRoomGuid);
                    dictKeyBySessionGuid.Remove(sessionGuid);
                    Logger.debug("EqptRoomHub: eqpt room guid {0} with session {1} has removed from IocpSessionMap.",
                        eqptRoomGuid, sessionGuid);
                }
            }
            catch (System.Exception ex)
            {
                Logger.debug("EqptRoomHub: eqpt room guid {0} removed with error {1}.",
                    eqptRoomGuid, ex.Message);
            }
        }

        public void removeBySessionGuid(Guid sessionGuid)
        {
            try
            {
                lock (dictLocker)
                {
                    if(dictKeyBySessionGuid.ContainsKey(sessionGuid))
                    {
                        Guid eqptRoomGuid = dictKeyBySessionGuid[sessionGuid];
                        dictKeyBySessionGuid.Remove(sessionGuid);
                        dictKeyByEqptRoomGuid.Remove(eqptRoomGuid);
                        Logger.debug("EqptRoomHub: eqpt room guid {0} with session {1} has removed from IocpSessionMap.",
                            eqptRoomGuid, sessionGuid);
                    }
                    else
                    {
                        Logger.debug("EqptRoomHub: try to remove session {0} but not found, it may not registered.",
                            sessionGuid);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Logger.debug("EqptRoomHub: session guid {0} removed with error {1}.",
                    sessionGuid, ex.Message);
            }
        }
    }
}
