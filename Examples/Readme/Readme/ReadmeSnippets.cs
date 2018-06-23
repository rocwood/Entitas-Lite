using Entitas;
using UnityEngine;

namespace Readme {

    public static class ReadmeSnippets {

        public static Entity CreateRedGem(this Context context, Vector3 position) {
            var entity = context.CreateEntity();
				entity.Add<GameBoardElementComponent>();
				entity.Add<MovableComponent>();
				entity.Add<PositionComponent>().value = position;
				entity.Add<AssetComponent>().name = "RedGem";
				entity.Add<InteractiveComponent>();

			return entity;
        }

        static void moveSystem() {
            var entities = Context<Game>.AllOf<PositionComponent, VelocityComponent>().GetEntities();
            foreach (var e in entities) {
				var vel = e.Get<VelocityComponent>();
				var pos = e.Modify<PositionComponent>();
				pos.value += vel.value;
            }
        }

        static void entityExample(Entity entity) {
            entity.Add<PositionComponent>().value = new Vector3(1, 2, 3);
            entity.Add<HealthComponent>().value = 100;
            entity.Add<MovableComponent>();

            entity.Modify<PositionComponent>().value = new Vector3(10, 20, 30);
            entity.Modify<HealthComponent>().value -= 1;
            entity.Remove<MovableComponent>();

            entity.Remove<PositionComponent>();

            var hasPos = entity.Has<PositionComponent>();
            var movable = entity.Has<MovableComponent>();
        }

        static void contextExample() {
            // contexts.context is kindly generated for you by the code generator
            var contexts = Contexts.sharedInstance;
            var context = contexts.GetContext<Game>();
            var entity = context.CreateEntity();
            entity.Add<MovableComponent>();

            // Returns all entities having MovableComponent and PositionComponent.
            // Matchers are also generated for you.
            var entities = context.GetEntities(Matcher<Game>.AllOf<MovableComponent, PositionComponent>());
            foreach (var e in entities) {
                // do something
            }
        }

        static void groupExample(Context context) {
            context.GetGroup(Matcher<Game>.AllOf<PositionComponent>()).GetEntities();

            // ----------------------------

            context.GetGroup(Matcher<Game>.AllOf<PositionComponent>()).OnEntityAdded += (group, entity, index, component) => {
                // Do something
            };
        }

        static void collectorExample(Context context) {
            var group = context.GetGroup(Matcher<Game>.AllOf<PositionComponent>());
            var collector = group.CreateCollector(GroupEvent.Added);

            // ----------------------------
            foreach (var e in collector.collectedEntities) {
                // do something
            }
            collector.ClearCollectedEntities();


            // ----------------------------
            collector.Deactivate();
        }

		static void monitorExample() {
			var group = Context<Game>.AllOf<PositionComponent>();
			var monitor = group.OnAdded(
				entities => {
					foreach (var e in entities) {
						// do something
					}
				}	
			);

			monitor.Execute();
		}

        static void positionComponent(Entity e, PositionComponent component, Vector3 position, Vector3 newPosition) {
            var pos = e.Get<PositionComponent>();
            var has = e.Has<PositionComponent>();

            e.Add<PositionComponent>().value = position;
            e.ReplaceNew<PositionComponent>().value = newPosition;
			e.Modify<PositionComponent>().value = newPosition;
			e.Remove<PositionComponent>();
        }

        #pragma warning disable
        static void userComponent(Context context, UserComponent component) {
            var e = context.GetSingleEntity<UserComponent>();
			var has = (e != null);
			var user = e.Get<UserComponent>();
        }

        static void movableComponent(Entity e) {
            var movable = e.Has<MovableComponent>();
            e.Add<MovableComponent>();
            e.Remove<MovableComponent>();
        }

        static void animatingComponent(Context context) {
            var e = context.GetSingleEntity<AnimatingComponent>();
			var isAnimating = (e != null);
        }
    }
}
