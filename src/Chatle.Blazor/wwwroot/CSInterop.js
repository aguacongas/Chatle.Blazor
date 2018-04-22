(function (chatle) {
    const assemblyName = 'Chatle.Blazor';
    const namespace = 'Chatle.Blazor';
    const typeName = 'OnKeyUp';
    const methodName = 'Handler';

    let onkeyupMethod;
    function onKeyUp(element, evt) {
        if (!onkeyupMethod) {
            onkeyupMethod = Blazor.platform.findMethod(
                assemblyName,
                namespace,
                typeName,
                methodName
            );
        }
        const value = Blazor.platform.toDotNetString(element.value);
        Blazor.platform.callMethod(onkeyupMethod, null, [value]);
    }

    chatle.onKeyUp = onKeyUp;
    window.chatle = chatle;
}) (window.chatle || {});
