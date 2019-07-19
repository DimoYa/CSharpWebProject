function wrapInASpan(text, className) {

    text.forEach(function (item) {
        $(`span, li:contains('${item}')`).html(function (_, html) {
            return html.replace(item, `<span class="${className}">${item}</span>`);
        });
    });

}