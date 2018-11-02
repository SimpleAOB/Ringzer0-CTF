//######################################
//This JS runs in the same window as the challenge via the dev tools
//######################################

var words = ($('body > div.container > div > div.challenge-wrapper > div.message')[0].innerText);
words = words.substring("----- BEGIN WORDS -----".length, words.length-("----- END WORDS -----".length)-1).trim();
words = words.split(',');

var lock = words.length;
var wordsDone = [];
for (var word in words) {
    (function (num) {
        $.ajax({
            url: "https://wordunscrambler.me/unscramble",
            method: "POST",
            data: {
                letters:words[num],
                dictionary:"sowpods",
                prefix:"",
                suffix:"",
                repeat:"yes",
            }
        }).done(function(res) {
            lock--;
            var x = res.substr(res.indexOf("wordWrapper")+13,words[num].length);
            wordsDone[num] = x;
        });
    })(word);
}

var inte = setInterval(() => {
    if (lock < 1) {
        clearInterval(inte);
        var wordstr = wordsDone.join(',');
        if (wordstr.indexOf(">") > 0) {alert("reload and try again, something went wrong");}
        else {
            window.location += "/"+encodeURIComponent(wordstr);
        }
        window.location += "/"+wordstr;
    }
}, 5);
