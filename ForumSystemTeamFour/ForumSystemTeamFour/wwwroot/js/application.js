var Messenger = function (el) {
    'use strict';
    var m = this;

    m.init = function () {
        //scramble characters
        m.codeletters = "꒘ꀆꀢꁏꁜꁽꂊꂞꂔꃍꄃꅵꅺꇻꈠꈾꉿꊎꋞꌀꌝꌲꎐ⧍⨳ᚡᚥᚼᛪꐪꒌ፠⚴♅⚴ϠϠϗϿѮꑔও৯ৼਲ਼ଈଡଵଡ଼୰ಀ೫෦እᖗᘙᙋᙽ◈▣◬๑๏߷ߧⵕⴲⴼꈔⴻⵁⵛꔮꔀꔊꔍꔧꔭꔯꕉꕙꕿꖥꗝꗞꗣꗫ꘨Ⱉ⨕⨊⩙⧰ꈔᛪᏫϠ";
        m.message = 0;
        m.current_length = 0;
        m.fadeBuffer = false;
        m.messages = [    
            'PARANORMALITY' //Final message
        ];

        //How fast the initial message appears on the screen, lower is faster
        setTimeout(m.animateIn, 0);
    };

    m.generateRandomString = function (length) {
        //startingMessage
        var random_text = '߷ꔍᚥਲ਼ꕉ⩙ꅵꕿꌀϠ፠ꈾ⨳';
        while (random_text.length < length) {
            random_text += m.codeletters.charAt(Math.floor(Math.random() * m.codeletters.length));
        }

        return random_text;
    };

    m.animateIn = function () {
        if (m.current_length < m.messages[m.message].length) {
            m.current_length = m.current_length + 2;
            if (m.current_length > m.messages[m.message].length) {
                m.current_length = m.messages[m.message].length;
            }

            var message = m.generateRandomString(m.current_length);
            $(el).html(message);
            //How long the initial message stays on screen before the animation starts, 400 is about 3 seconds
            setTimeout(m.animateIn, 400);
        } else {
            setTimeout(m.animateFadeBuffer, 20);
        }
    };

    m.animateFadeBuffer = function () {
        if (m.fadeBuffer === false) {
            m.fadeBuffer = [];
            for (var i = 0; i < m.messages[m.message].length; i++) {
                //How fast the real character shows up, higher is slower, 50 seems ok
                m.fadeBuffer.push({ c: (Math.floor(Math.random() * 50)) + 1, l: m.messages[m.message].charAt(i) });
            }
        }

        var do_cycles = false;
        var message = '';

        for (var i = 0; i < m.fadeBuffer.length; i++) {
            var fader = m.fadeBuffer[i];
            if (fader.c > 0) {
                do_cycles = true;
                fader.c--;
                message += m.codeletters.charAt(Math.floor(Math.random() * m.codeletters.length));
            } else {
                message += fader.l;
            }
        }

        $(el).html(message);

        if (do_cycles === true) {
            //How fast the characters change, higher is slower, 120 seems ok
            setTimeout(m.animateFadeBuffer, 120);
        }
    };

    m.init();
}

new Messenger($('#messenger'));

var MessengerTwo = function (el) {
    'use strict';
    var m = this;

    m.init = function () {
        //scramble characters
        m.codeletters = "꒘ꀆꀢꁏꁜꁽꂊꂞꂔꃍꄃꅵꅺꇻꈠꈾꉿꊎꋞꌀꌝꌲꎐ⧍⨳ᚡᚥᚼᛪꐪꒌ፠⚴♅⚴ϠϠϗϿѮꑔও৯ৼਲ਼ଈଡଵଡ଼୰ಀ೫෦እᖗᘙᙋᙽ◈▣◬๑๏߷ߧⵕⴲⴼꈔⴻⵁⵛꔮꔀꔊꔍꔧꔭꔯꕉꕙꕿꖥꗝꗞꗣꗫ꘨Ⱉ⨕⨊⩙⧰ꈔᛪᏫϠ";
        m.message = 0;
        m.current_length = 0;
        m.fadeBuffer = false;
        m.messages = [
            'ꁜꁽꂊꂞꂔꃍꄃꅵꅺꇻꈠꈾꉿꊎꋞꌀꌝꌲꎐ' //Final message
        ];

        //How fast the initial message appears on the screen, lower is faster
        setTimeout(m.animateIn, 0);
    };

    m.generateRandomString = function (length) {
        //startingMessage
        var random_text = 'See you again soon!';
        while (random_text.length < length) {
            random_text += m.codeletters.charAt(Math.floor(Math.random() * m.codeletters.length));
        }

        return random_text;
    };

    m.animateIn = function () {
        if (m.current_length < m.messages[m.message].length) {
            m.current_length = m.current_length + 2;
            if (m.current_length > m.messages[m.message].length) {
                m.current_length = m.messages[m.message].length;
            }

            var message = m.generateRandomString(m.current_length);
            $(el).html(message);
            //How long the initial message stays on screen before the animation starts, 400 is about 3 seconds
            setTimeout(m.animateIn, 400);
        } else {
            setTimeout(m.animateFadeBuffer, 0);
        }
    };

    m.animateFadeBuffer = function () {
        if (m.fadeBuffer === false) {
            m.fadeBuffer = [];
            for (var i = 0; i < m.messages[m.message].length; i++) {
                //How fast the real character shows up, higher is slower, 50 seems ok
                m.fadeBuffer.push({ c: (Math.floor(Math.random() * 5000)) + 1, l: m.messages[m.message].charAt(i) });
            }
        }

        var do_cycles = false;
        var message = '';

        for (var i = 0; i < m.fadeBuffer.length; i++) {
            var fader = m.fadeBuffer[i];
            if (fader.c > 0) {
                do_cycles = true;
                fader.c--;
                message += m.codeletters.charAt(Math.floor(Math.random() * m.codeletters.length));
            } else {
                message += fader.l;
            }
        }

        $(el).html(message);

        if (do_cycles === true) {
            //How fast the characters change, higher is slower, 120 seems ok
            setTimeout(m.animateFadeBuffer, 120);
        }
    };

    m.init();
}

new MessengerTwo($('#farewellMessenger'));