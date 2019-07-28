function twoColumnDropDown(dd, separatorChars, magicNumber, reduceLen) {
    var biggestLength = 0;
    $(dd).each(function () {
        var len = $(this).text().length - reduceLen;
        if (len > biggestLength) {
            biggestLength = len;
        }
    });
    var padLength = biggestLength + magicNumber;
    $(dd).each(function () {
        var parts = $(this).text().split(separatorChars);
        var strLength = parts[0].length;
        for (var x = 0; x < (padLength - strLength); x++) {
            parts[0] = parts[0] + ' ';
        }
        
        $(this).text(parts[0].replace(/ /g, '\u00a0') + parts[1]).text;
    });
}