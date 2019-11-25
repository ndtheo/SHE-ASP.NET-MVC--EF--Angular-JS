myApp.directive("fileModel",
	[
		"$parse",
		$parse => ({
			restrict: "A",
			link(scope, element: any, attrs: any) {
				var model = $parse(attrs.fileModel);
				var modelSetter = model.assign;

				element.bind("change",
					() => {
						scope.$apply(() => {
							modelSetter(scope, element[0].files[0]);
						});
					}
				);
			}
		})
	]
);

myApp.directive('filesModel', ['$parse', function ($parse) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			var model = $parse(attrs.filesModel);
			console.log(attrs.filesModel);
			var isMultiple = attrs.multiple;
			var modelSetter = model.assign;

			element.bind('change', function () {
				var values = [];
				var input = (element[0] as HTMLInputElement);
				angular.forEach(input.files, function (file) {
					var value = {
						id: getUniqueIdForSession(),
						name: file.name,
						size: file.size,
						url: URL.createObjectURL(file),
						_file: file
					};
					values.push(value);
				});
				scope.$apply(function () {
					if (isMultiple) {
						var existingFiles = getExistingPhotos(scope, attrs.filesModel);
						modelSetter(scope, values.concat(existingFiles));
					} else {
						modelSetter(scope, values[0]);
					}
				});
				input.form.reset();
			});
		}
	};
}]);

var uniqueIdForSession = 0;
function getUniqueIdForSession() {
	uniqueIdForSession++;
	return uniqueIdForSession;
}


function getExistingPhotos(scope, modelName: string) {
	var names = modelName.split(".");
	var data = scope;

	names.forEach(n => data = data[n]);
	return data;
}