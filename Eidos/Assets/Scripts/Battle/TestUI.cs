using Eidos.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Eidos.Battles
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private Text currentStateText;

        private EventBinding<StartBattle> onStartBattle;
        private EventBinding<ChooseAction> onChooseAction;
        private EventBinding<ResolveAction> onResolveAction;
        private EventBinding<EndBattle> onEndBattle;
        private EventBinding<ExitBattle> onExitBattle;

        private void OnEnable()
        {
            onStartBattle = new EventBinding<StartBattle>(SetStartText);
            EventBus<StartBattle>.Register(onStartBattle);

            onChooseAction = new EventBinding<ChooseAction>(SetChooseText);
            EventBus<ChooseAction>.Register(onChooseAction);

            onResolveAction = new EventBinding<ResolveAction>(SetResolveText);
            EventBus<ResolveAction>.Register(onResolveAction);

            onEndBattle = new EventBinding<EndBattle>(SetEndText);
            EventBus<EndBattle>.Register(onEndBattle);

            onExitBattle = new EventBinding<ExitBattle>(SetExitText);
            EventBus<ExitBattle>.Register(onExitBattle);
        }

        private void OnDisable()
        {
            EventBus<StartBattle>.Deregister(onStartBattle);
            EventBus<ChooseAction>.Deregister(onChooseAction);
            EventBus<ResolveAction>.Deregister(onResolveAction);
            EventBus<EndBattle>.Deregister(onEndBattle);
            EventBus<ExitBattle>.Deregister(onExitBattle);
        }

        private void SetStartText() => currentStateText.text = "Battle Started";
        private void SetChooseText() => currentStateText.text = "Choosing Action";
        private void SetResolveText() => currentStateText.text = "Resolving Action";
        private void SetEndText() => currentStateText.text = "Ended Battle";
        private void SetExitText() => currentStateText.text = "Exited Battle";
    }
}
