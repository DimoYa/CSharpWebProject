function twoColumnDropDown(dd, separatorChars, magicNumber, reduceLen) {
    let biggestLength = 0;
    $(dd).each(function () {
        let len = $(this).text().length - reduceLen;
        if (len > biggestLength) {
            biggestLength = len;
        }
    });
    let padLength = biggestLength + magicNumber;
    $(dd).each(function () {
        let parts = $(this).text().split(separatorChars);
        let strLength = parts[0].length;
        for (let x = 0; x < (padLength - strLength); x++) {
            parts[0] = parts[0] + ' ';
        }
        
        $(this).text(parts[0].replace(/ /g, '\u00a0') + parts[1]).text;
    });
}