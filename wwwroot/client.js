// get container width and height
const container = document.getElementById('container');
const width = container.clientWidth;
const height = container.clientHeight;
let maxId = 0;
let intervalId;

window.onload = function() {
    getData('api/words');
    intervalId = setInterval( () => {
        getData('api/words/after/' + maxId);
    }, 3000 );
}

function getData(url) {
    fetch(url)
        .then(response => response.json())
        .then(data => placeWords(data))
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
        });
}

function placeWords(words){
    if (words) {
        words.forEach(word => {
            const id = word.id;
            container.innerHTML += '<div style="opacity:0;color:' + word.color + '" class="word in" id="' + id + '">' + word.word + '</div>';
            const div = document.getElementById(id);
            // update top and left position
            div.style.top = getRandomNumber(0, height - div.clientHeight) +"px";
            div.style.left = getRandomNumber(0, width - div.clientWidth) +"px";
            maxId = maxId >= id ? maxId : id;
        });
        const divs = document.getElementsByClassName('word');
        for ( let i=0; i < divs.length; i++ ) {
            const div = divs[i];
            div.style.opacity = .75;
        }
    }
}

function getRandomNumber(min, max) {
    return Math.random() * (max - min) + min;
}

document.getElementById('container').onclick = function(event) {
    const obj = event.target.closest('div')
    if (obj.classList.contains('word')){
        fetch('api/words/' + obj.id, { method: 'DELETE', })
            .then(res => res.status)
            .then(() => {
                clearInterval(intervalId);
                obj.classList.remove('in');
                obj.classList.add('out');
                obj.style.opacity = 0;
                obj.addEventListener("transitionend", () => {
                    obj.remove();
                    intervalId = setInterval( () => {
                        getData('api/words/after/' + maxId);
                    }, 3000 );
                })
            }).catch(error => {
                console.error('There has been a problem with your fetch operation:', error);
            });
    }
};
