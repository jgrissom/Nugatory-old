// get container width and height
const container = document.getElementById('container');
const width = container.clientWidth;
const height = container.clientHeight;

window.onload = function() {
    getData('api/word');
}

// get initial data from db using AJAX
function getData(url) {
    fetch(url)
        .then(response => response.json())
        .then(data => placeWords(data))
        .catch(error => {
            console.error('There has been a problem with your fetch operation:', error);
        });
}

// place words in window at random top, left
function placeWords(words){
    console.log(words);
    if (words) {
        words.forEach(word => {
            const id = word.id;
            container.innerHTML += '<div style="opacity:0;color:' + word.color + '" class="word in" id="' + id + '">' + word.word + '</div>';
            const div = document.getElementById(id);
            // update top and left position
            div.style.top = getRandomNumber(0, height - div.clientHeight) +"px";
            div.style.left = getRandomNumber(0, width - div.clientWidth) +"px";
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

// delegated event listener attached to click event for each word
document.getElementById('container').onclick = function(event) {
    const obj = event.target.closest('div')
    if (obj.classList.contains('word')){
        // AJAX call to delete the word from DB
        event.preventDefault();
        fetch('api/word/' + obj.id, { method: 'DELETE', })
            .then(res => res.status)
            .then(() => {
                console.log('deleted');
            }).catch(error => {
                console.error('There has been a problem with your fetch operation:', error);
            });
    }
};
