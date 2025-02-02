using Content.Shared.Light.Components;
using Content.Shared.SurveillanceCamera;
using Robust.Shared.Serialization;

namespace Content.Shared.Silicons.StationAi;

public abstract partial class SharedStationAiSystem
{
    // Handles light toggling.

    private void InitializeLight()
    {
        SubscribeLocalEvent<ItemTogglePointLightComponent, StationAiLightEvent>(OnLight);
        SubscribeLocalEvent<SurveillanceCameraVisuals, StationAiLightEvent>(OnView);
    }

    private void OnLight(EntityUid ent, ItemTogglePointLightComponent component, StationAiLightEvent args)
    {
        if (args.Enabled)
            _toggles.TryActivate(ent, user: args.User);

        else
            _toggles.TryDeactivate(ent, user: args.User);
    }

    private void OnView(EntityUid ent, SurveillanceCameraVisuals component, StationAiLightEvent args)
    {
        var key = SurveillanceCameraVisuals.Disabled;

        if (args.Enabled)
            key = SurveillanceCameraVisuals.AIUse;

        else
           return;

        _appearance.SetData(ent, SurveillanceCameraVisualsKey.Key, key);
    }

[Serializable, NetSerializable]
public sealed class StationAiLightEvent : BaseStationAiAction
{
    public bool Enabled;
}
