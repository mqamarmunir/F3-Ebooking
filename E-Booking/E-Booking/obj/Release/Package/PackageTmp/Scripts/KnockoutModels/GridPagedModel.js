function GridPagedModel(data, pageSize, startIndex) {
    var self = this;
    //debugger;
    // stuff that matters
    // ---------------------------------------------
    self.items = ko.observableArray(data);

    // pager related stuff
    // ---------------------------------------------
    self.totalRecords = data.length;
    self.currentPage = ko.observable((startIndex == 0 || startIndex == null || startIndex == undefined ? 1 : startIndex));
    self.perPage = (pageSize == 0 || pageSize == null || pageSize == undefined ? 10 : pageSize);

    self.pagedItems = ko.computed(function () {
        var pg = self.currentPage(),
            start = self.perPage * (pg - 1),
            end = start + self.perPage;
        return self.items().slice(start, end);
    }, self);

    self.nextPage = function () {
        if (self.nextPageEnabled())
            self.currentPage(self.currentPage() + 1);
    };

    self.nextPageEnabled = ko.computed(function () {
        return self.items().length > self.perPage * self.currentPage();
    }, self);

    self.previousPage = function () {
        if (self.previousPageEnabled())
            self.currentPage(self.currentPage() - 1);
    };

    self.previousPageEnabled = ko.computed(function () {
        return self.currentPage() > 1;
    }, self);

    self.previousPage = function () {
        if (self.previousPageEnabled())
            self.currentPage(self.currentPage() - 1);
    };

    self.jumpToFirstPage = function () {
        self.currentPage(1);
    };

    self.jumpToLastPage = function () {
        self.currentPage(self.totalPages);
    };
    
    self.totalPages = Math.ceil(self.totalRecords / self.perPage);

    self.errors = ko.validation.group(self);
}