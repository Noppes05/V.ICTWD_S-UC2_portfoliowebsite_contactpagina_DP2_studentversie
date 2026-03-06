window.addEventListener('contextmenu', e => e.preventDefault()); 

function naiveEmailCheck(email) {
    return /@/.test(email);
}

function setupValidation() {
    const form = document.getElementById('contactForm');
    const hp = document.getElementById('website');
    const email = document.getElementById('Email');
    const name = document.getElementById('Naam');
    const msg = document.getElementById('Bericht');
    const status = document.getElementById('liveStatus');
    const submit = document.getElementById('submit');
    const errors = { emailErr: false, nameErr: false, msgErr: false };

    const echo = (id, value) => {
        document.getElementById(id).innerText = `\n Probleem met: ${value}, invoer niet lang genoeg\n `;
        errors[id] = true;
        submit.disabled = true;
    };
    const Noecho = (id, value) => {
        document.getElementById(id).innerText = ``;
        errors[id] = false;
        if (Object.values(errors).every(v => !v)) {
            submit.disabled = false;
        }
    };

    [email, name, msg].forEach(el => {
        el.addEventListener('input', () => {
            if (el === email && !naiveEmailCheck(el.value)) {
                echo('emailErr', el.value);
            }
            else if (el === email && naiveEmailCheck(el.value)) {
                Noecho('emailErr', el.value);
            
            } else if (el === name && el.value.length < 2) {
                echo('nameErr', el.value);
            } else if (el === name && el.value.length > 2) {
                Noecho('nameErr', el.value);
            } else if (el === msg && el.value.length < 5) {
                echo('msgErr', el.value);
            }
            else if (el === msg && el.value.length > 5) {
                Noecho('msgErr', el.value);
            }
        });
    });

    form.addEventListener('submit', (e) => {
        console.log("submit clicked")
        if (hp.value) {
            e.preventDefault();
            alert('Spam gedetecteerd (client-side)!');
            return false;
        }

        return true;
    });
}

window.addEventListener('DOMContentLoaded', setupValidation);