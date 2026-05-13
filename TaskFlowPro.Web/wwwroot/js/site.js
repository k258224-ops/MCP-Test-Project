function showToast(message, type = 'success') {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.innerHTML = `
        <div class="toast-content">
            <i class="fas ${type === 'success' ? 'fa-check-circle' : 'fa-exclamation-circle'}"></i>
            <span>${message}</span>
        </div>
    `;
    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transform = 'translateX(100%)';
        setTimeout(() => toast.remove(), 300);
    }, 3000);
}

function showLoading(btnId) {
    const btn = document.getElementById(btnId);
    if (!btn) return;
    const originalText = btn.innerHTML;
    btn.disabled = true;
    btn.innerHTML = '<div class="spinner"></div>';
    return originalText;
}

function hideLoading(btnId, originalText) {
    const btn = document.getElementById(btnId);
    if (!btn) return;
    btn.disabled = false;
    btn.innerHTML = originalText;
}

// Client side validation enhancement
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', function() {
        const submitBtn = this.querySelector('button[type="submit"]');
        if (submitBtn && !submitBtn.id) {
            submitBtn.id = 'submit-' + Math.random().toString(36).substr(2, 9);
        }
        if (submitBtn) {
            this._originalText = showLoading(submitBtn.id);
        }
    });
});
