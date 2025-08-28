export async function initSubmitConfrim() {
    document.getElementById('voluntaryButton').addEventListener('click', async function () {
        window.open('/NotificationCoverNote/Voluntary', '_blank');
    });

    document.getElementById('compulsoryButton').addEventListener('click', async function () {
        window.open('/NotificationCoverNote/Compulsory', '_blank');
    });
}