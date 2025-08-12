export function pageLoadFadeIn() {
    $("#overlay").fadeIn(300);
}

export function pageLoadFadeOut(timeout) {
    setTimeout(function () {
        $("#overlay").fadeOut(300);
    }, timeout);
}

// Utility function for delay
export function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

// Alternative: Using Web Animations API (more modern)
export async function pageLoadFadeInModern() {
    const overlay = document.querySelector("#overlay");

    if (!overlay) return;

    overlay.style.display = 'block';

    const animation = overlay.animate([
        { opacity: 0 },
        { opacity: 1 }
    ], {
        duration: 300,
        fill: 'forwards'
    });

    await animation.finished;
}

export async function pageLoadFadeOutModern(timeout = 0) {
    if (timeout > 0) {
        await delay(timeout);
    }

    const overlay = document.querySelector("#overlay");

    if (!overlay) return;

    const animation = overlay.animate([
        { opacity: 1 },
        { opacity: 0 }
    ], {
        duration: 300,
        fill: 'forwards'
    });

    await animation.finished;
    overlay.style.display = 'none';
}