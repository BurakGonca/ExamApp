function simulateEnterKeyPress() {
    const searchInput = document.getElementById('searchInput');

    if (searchInput) {
        const keyUpEvent = new KeyboardEvent('keyup', {
            key: 'Enter',
            keyCode: 13,
            which: 13,
            bubbles: true
        });
        searchInput.dispatchEvent(keyUpEvent);
    }
}
window.addEventListener('load', simulateEnterKeyPress);
window.addEventListener('pageshow', simulateEnterKeyPress);