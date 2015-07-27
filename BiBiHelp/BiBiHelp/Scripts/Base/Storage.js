// прокси для localStorage. Если он не доступен в браузере, то прокси ничего не будет делать, а на запросы будут возвращать undefined
; window.Storage = (function () {
    var currentHashName = "currentHash", // имя для сохрания хэша актуальной версии
        storageIdentificator = "unicloud - "; // добавляется во все ключи для их идентификации 

    var storage = function (mainHash) {
        if (isLocalStorageAvailable() && mainHash && !version.debugMode) {
            initStorageMethods(); // инициализируем методы только если localStorage доступен, иначе останутся пустышки
            var currentHash = getFromStorage(currentHashName);
            if (currentHash != mainHash) {
                this.clear();
            }
            setToStorage(currentHashName, mainHash);
        }
    };

    storage.prototype.set = function () {
        console.log("не сохранено");
    };

    storage.prototype.get = function () {
        return undefined;
    };

    storage.prototype.clear = function () { };

    function initStorageMethods() {
        storage.prototype.set = setToStorage;
        storage.prototype.get = getFromStorage;
        storage.prototype.clear = clearLocalStorage;
    }

    function clearLocalStorage() {
        var keysForRemove = [];
        for (var i = 0, length = localStorage.length; i < length; ++i) {
            var key = localStorage.key(i);
            if (key.search(storageIdentificator) !== -1) {
                keysForRemove.push(key);

            }
        }
        for (i = 0, length = keysForRemove.length; i < length; ++i) {
            localStorage.removeItem(keysForRemove[i]);
        }
    }

    function getFromStorage(key) {
        return localStorage[storageIdentificator + key];
    };

    function setToStorage(key, value) {
        try {
            localStorage[storageIdentificator + key] = value;
        }
        catch (ex) {
            console.log("Ошибка при записи в localStorage", ex);
        }
    };

    function isLocalStorageAvailable() {
        try {
            return 'localStorage' in window && window['localStorage'] !== null;
        } catch (e) {
            return false;
        }
    }

    return storage;
})();