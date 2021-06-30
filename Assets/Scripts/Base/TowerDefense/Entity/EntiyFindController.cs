using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class EntityController
{
        static List<EntityController> entities = new List<EntityController>();

        public static EntityController getTarget(IFighter fighter, IGetTargetRange getTargetAuto)
        {
            EntityController target = null;
            float minDistance = 10000;
            foreach (EntityController entity in entities)
            {
                float distance = Vector3.Distance(fighter.FighterPos, entity.transform.position);
                if (distance >= getTargetAuto.MinTargetRange && distance <= getTargetAuto.MaxTargetRange && (target == null || minDistance > distance) && fighter.CheckCanAttack(entity))
                {
                    target = entity;
                    minDistance = distance;
                }
            }
            return target;
        }

        public static List<EntityController> getTargets(IFighter fighter, IGetTargetsAuto getTargetsAuto)
        {
            List<Target> targets = new List<Target>();
            if (getTargetsAuto.numTarget == 0) return null;
            if (getTargetsAuto.numTarget == 1)
            {
                List<EntityController> entities = new List<EntityController>();
                entities.Add(getTarget(fighter, getTargetsAuto));
                return entities;
            }

            foreach (EntityController entity in entities)
            {
                float distance = Vector3.Distance(fighter.FighterPos, entity.transform.position);

                if (distance < getTargetsAuto.MinTargetRange || distance > getTargetsAuto.MaxTargetRange || !fighter.CheckCanAttack(entity)) continue;
                if (targets.Count == getTargetsAuto.numTarget)
                {
                    float maxDistance = 0;

                    Target removeTarget = null;

                    foreach (Target target in targets)
                    {
                        if (target.distance > distance && target.distance > maxDistance)
                        {
                            removeTarget = target;
                        }
                    }
                    if (removeTarget == null)
                    {
                        continue;
                    }
                    targets.Remove(removeTarget);
                }
                targets.Add(new Target(distance, entity));
            }
            return Target.GetEntities(targets);
        }
}

