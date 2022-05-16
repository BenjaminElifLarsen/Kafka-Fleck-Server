import Vue from 'vue'
//import App from './App.vue'
import operatormode from './components/operator-mode.vue';
import managermode from './components/manager-mode.vue';

Vue.config.productionTip = false

const routes = {
  '' : operatormode,
  '#': operatormode,
  '#manager': managermode
}
let vue = new Vue({
  data: {
    currentRoute : window.location.hash,
    rev: "",
  },
  computed: {
    ViewComponent () {
      return routes[this.currentRoute] || operatormode
    }
  },
  methods: {
    save(newStoreItems) {
      var PouchDB = require("pouchdb-browser").default; //npm install --save pouchdb-browser
      var db = new PouchDB("possum");
      var self = this;
      db.put({
        _id: "items",
        items: newStoreItems,
        _rev: this.rev,
      })
        .then(function () {
          console.log("Saved");
          self.load(function() {});
        })
        .catch(function (err) {
          console.error(err);
        });
    },
    load(callback) {
      var PouchDB = require("pouchdb-browser").default;
      var db = new PouchDB("possum");
      var self = this;
      if (!callback) console.error("Missing callback")
      db.get("items").then(function (doc) {
        self.rev = doc._rev;
        callback(doc.items);
      });
    }
  },
  render(h) { return h(this.ViewComponent) }
}).$mount('#app')

window.onhashchange = function() {
  vue.currentRoute = window.location.hash;
}