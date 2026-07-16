/**
 * Veloria Admin - Shell JS
 * Shared across every Admin page: sidebar toggle, notifications dropdown,
 * generic modal open/close helpers, and a small fetch() wrapper that
 * automatically attaches the ASP.NET Core antiforgery token to AJAX POSTs.
 */

// ── Sidebar ──────────────────────────────────────────────────────────────────
function toggleSidebar() {
    document.getElementById('sidebar')?.classList.toggle('open');
    document.getElementById('sidebar-overlay')?.classList.toggle('open');
}

function closeSidebar() {
    document.getElementById('sidebar')?.classList.remove('open');
    document.getElementById('sidebar-overlay')?.classList.remove('open');
}

// ── Notifications dropdown ──────────────────────────────────────────────────
document.addEventListener('DOMContentLoaded', function () {
    var notifBtn = document.getElementById('notification-btn');
    var notifDropdown = document.getElementById('notifications-dropdown');

    if (notifBtn && notifDropdown) {
        notifBtn.addEventListener('click', function (e) {
            e.stopPropagation();
            notifDropdown.classList.toggle('show');
        });

        document.addEventListener('click', function (e) {
            if (!notifDropdown.contains(e.target) && e.target !== notifBtn) {
                notifDropdown.classList.remove('show');
            }
        });
    }
});

function clearAllNotifications() {
    var list = document.getElementById('notifications-list');
    var count = document.getElementById('notification-count');
    if (list) list.innerHTML = '';
    if (count) count.style.display = 'none';
}

// ── Generic modal helpers ───────────────────────────────────────────────────
function openModal(id) {
    document.getElementById(id)?.classList.add('open');
}

function closeModal(id) {
    document.getElementById(id)?.classList.remove('open');
}

// ── AJAX helper ──────────────────────────────────────────────────────────────
/**
 * Wrapper around fetch() that:
 *  - JSON-encodes the body automatically
 *  - Attaches the antiforgery token header for POST requests
 *  - Throws a normalized Error with server-provided message on failure
 */
async function veloriaFetch(url, options) {
    options = options || {};
    var headers = Object.assign({}, options.headers || {});

    if (options.method && options.method.toUpperCase() !== 'GET') {
        var token = document.querySelector('meta[name="request-verification-token"]')?.content;
        if (token) {
            headers['RequestVerificationToken'] = token;
        }
    }

    if (options.json !== undefined) {
        headers['Content-Type'] = 'application/json';
        options.body = JSON.stringify(options.json);
        delete options.json;
    }

    var response = await fetch(url, Object.assign({}, options, { headers: headers }));

    if (!response.ok) {
        var message = 'Request failed (' + response.status + ').';
        try {
            var errorBody = await response.json();
            if (errorBody && errorBody.message) {
                message = errorBody.message;
            } else if (errorBody && errorBody.errors) {
                message = Object.values(errorBody.errors).flat().join(' ');
            }
        } catch (_) { /* response wasn't JSON, keep default message */ }
        throw new Error(message);
    }

    if (response.status === 204) {
        return null;
    }

    return response.json();
}

function formatCurrency(value) {
    return '$' + Number(value).toFixed(2);
}

function escapeHtml(value) {
    if (value === null || value === undefined) return '';
    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#039;');
}
