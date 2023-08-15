localStorage.setItem('FirstName', 'Dimple');
localStorage.setItem('Location', 'India');


let abd = "ABCDEFGH";
var name = 'fghjk';


console.log(localStorage.getItem('Location'));
console.log(localStorage.getItem('FirstName'));

sessionStorage.setItem('Location', 'London');
sessionStorage.setItem('xyz', 'abc');
sessionStorage.setItem(abd,name);
sessionStorage.removeItem('xyz');

console.log(sessionStorage.getItem('Location'));