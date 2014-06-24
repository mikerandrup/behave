Habits.Router.map(function() {
	this.resource('habits', { path: '/' }, function() {
		//child routes will go here.
		this.route('active');
		this.route('completed');
	});
});

Habits.HabitsRoute = Ember.Route.extend({
	model: function() {
		return this.store.find('habits');
	}
});

Habits.HabitsIndexRoute = Ember.Route.extend({
	model: function(){
		return this.modelFor('habits');
	}
});

Habits.HabitsActiveRoute = Ember.Route.extend({
	model: function(){
		return this.store.filter('todo', function(todo){
			return !todo.get('isCompleted');
		});
	},

	renderTemplate: function(controller){
		this.render('habits/index', {controller: controller});
	}
});

Habits.HabitsCompletedRoute = Ember.Route.extend({
	model: function (){
		return this.store.filter('todo', function (todo){
			return todo.get('isCompleted');
		});
	},

	renderTemplate: function (controller){
		this.render('habits/index', {controller: controller});
	}
});