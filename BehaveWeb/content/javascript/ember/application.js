window.Habits = Ember.Application.create();

Habits.ApplicationAdapter = DS.LSAdapter.extend({
	namespace: 'habits-emberjs'
});