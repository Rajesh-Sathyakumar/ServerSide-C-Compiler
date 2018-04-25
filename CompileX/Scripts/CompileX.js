
$('#compileProgram').on('click',
    function() {

        var code = $('#code').val();
        var codeLines = code.split('\n');

        var commandLineArgs = $('#commandLineArgs').val();

        var commandInputs = $('#commandLineInput').val();
        var commandLineInputs = commandInputs.split('\n');

        var data = { "commandLineArgs": [commandLineArgs], "inputString": commandLineInputs, "code": codeLines };

        settings = {
            "url": "http://localhost:55619/api/CompileXapi",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(data)
        };

        $.ajax(settings).done(function (response) {
            var result = response.join('<br/>\n');
            $('#programOutput').html(result);
        });
});
