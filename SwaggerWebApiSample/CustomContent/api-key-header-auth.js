(function () {
    $(function () {
        console.log("执行了")
        console.log($('#input_apiKey'))
        $('#input_apiKey').show();
        $('#input_apiKey').on('change', function () {
            var key = this.value;
            console.log("2")
            if (key && key.trim() !== '') {
                swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", key, "header"));
            }
        });
    });
})();