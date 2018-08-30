using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using NHibernate.Event; 
using ZLERP.Model;
using NHibernate.Persister.Entity;
using NHibernate.Engine;
using NHibernate.Intercept;
using System.Threading;

namespace ZLERP.NHibernateRepository
{
    /// <summary>
    /// Nhibernate 事件监听
    /// </summary>
    public class NhEventListener :  
        IPreUpdateEventListener, IPreInsertEventListener
    {
        #region IFlushEntityEventListener Members

            //public void  OnFlushEntity(FlushEntityEvent @event)
            //{
                
            //    var entity = @event.Entity;
            //    var entityEntry = @event.EntityEntry;

            //    if (entityEntry.Status == Status.Deleted)
            //    {
            //        return;
            //    }
            //    var trackable = entity as Entity;
            //    if (trackable == null)
            //    {
            //        return;
            //    }
            //    if (HasDirtyProperties(@event))
            //    {
                    
            //        trackable.ModifyTime = DateTime.Now;
            //        trackable.Modifier = GetUserId();
            //    }
            //}
            //private bool HasDirtyProperties(FlushEntityEvent @event)
            //{
            //    ISessionImplementor session = @event.Session;
            //    EntityEntry entry = @event.EntityEntry;
            //    var entity = @event.Entity;
            //    if (!entry.RequiresDirtyCheck(entity) || !entry.ExistsInDatabase || entry.LoadedState == null)
            //    {
            //        return false;
            //    }
            //    IEntityPersister persister = entry.Persister;

            //    object[] currentState = persister.GetPropertyValues(entity, session.EntityMode); ;
            //    object[] loadedState = entry.LoadedState;

            //    return persister.EntityMetamodel.Properties
            //        .Where((property, i) => !LazyPropertyInitializer.UnfetchedProperty.Equals(currentState[i]) && property.Type.IsDirty(loadedState[i], currentState[i], session))
            //        .Any();
            //}

        #endregion
        
        #region IPreUpdateEventListener Members

        public bool OnPreUpdate(PreUpdateEvent evt)
        {
            Entity entity = evt.Entity as Entity;
            entity.Modifier = GetUserId();
            entity.ModifyTime = DateTime.Now;
            SetState(evt.Persister, evt.State, "Modifier", entity.Modifier);
            SetState(evt.Persister, evt.State, "ModifyTime", entity.ModifyTime);
           // int [] dirteis = evt.Persister.FindDirty(evt.State, evt.OldState, evt.Entity, evt.Session);
            
            return false;
        }

        #endregion

        #region IPreInsertEventListener Members

        public bool OnPreInsert(PreInsertEvent evt)
        {
            Entity entity = evt.Entity as Entity;
            entity.Builder = GetUserId();
            entity.BuildTime = DateTime.Now;
            SetState(evt.Persister, evt.State, "Builder", entity.Builder);
            SetState(evt.Persister, evt.State, "BuildTime", entity.BuildTime); 
            return false;
        }
        #endregion

        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        string GetUserId()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string identityName = Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(identityName))
                    return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                else
                    return string.Empty;
            }
            else
                return string.Empty;

        }

        private void SetState(IEntityPersister persister,
            object[] state, string propertyName, object value)
        {
            var index = GetIndex(persister, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
        private int GetIndex(IEntityPersister persister,
          string propertyName)
        {
            return Array.IndexOf(persister.PropertyNames,
              propertyName);
        }
    }
}
