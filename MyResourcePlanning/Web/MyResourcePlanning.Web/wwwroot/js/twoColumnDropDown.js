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
        var strLength = parts[1].length;
        for (var x = 0; x < (padLength - strLength); x++) {
            parts[1] = parts[1] + ' ';
        }
        var strLengthLastChar = parts[2].length;
        for (var x = 0; x < (padLength - strLengthLastChar); x++) {
            parts[2] = parts[2] + ' ';
        }
        $(this).text(parts[1].replace(/ /g, '\u00a0') + parts[2].replace(/ /g, '\u00a0') + parts[3]).text;
    });
}
