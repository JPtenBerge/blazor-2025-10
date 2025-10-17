
function getTekst() {
    return 'werkt!';
}

function getObj() {
    return {
        x: 24,
        y: 'hoi'
    };
}

function getAsyncSpul() {
    return new Promise((resolve, reject) => {
       // setTimeout(() => {
       //     resolve('yay ook dit werkt');
       // }, 2000);
       
       navigator.geolocation.getCurrentPosition(
           position => resolve(position.coords),
           err => reject(err)
       );
    });
}