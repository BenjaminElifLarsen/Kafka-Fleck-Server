<template>
  <div class="ui container">
      <modeswitcher />
      <div v-if="pin_entered">
    <h1 class="ui dividing header">Manager mode</h1>
    <table class="ui celled table">
      <thead>
        <tr>
          <th>Item</th>
          <th>Price</th>
          <th>Stock</th>
          <th>Category</th>
          <th>Hidden</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in storeItems" v-bind:key="item.id">
          <td data-label="Item">
            <div class="ui input"><input
              class="ui input"
              type="text"
              v-model="item.name"
              @change="save"
            /></div>
          </td>
          <td data-label="Price">
            <div class="ui input"><input type="number" size="6" v-model="item.price" /></div>
          </td>
          <td data-label="Stock">
            <div class="ui input"><input type="number" size="6" v-model="item.stock" /></div>
          </td>
          <td><select v-model="item.category">
              <option v-for="cat in categories" v-bind:key="cat">{{cat}}</option>
            </select></td>
          <td>
            <div class="ui checked checkbox">
              <input type="checkbox" name="hidden" v-model="item.hidden" />
              <label></label>
            </div>
          </td>
          <td><input type="button" class="ui negative button" @click="remove(item)" value="Remove" /></td>
        </tr>

        <tr>
          <td><div class="ui input"><input class="ui input" type="text" v-model="newitem.name" /></div></td>
          <td><div class="ui input"><input type="number" size="6" v-model="newitem.price" /></div></td>
          <td><div class="ui input"><input type="number" size="6" v-model="newitem.stock" /></div></td>
          <td><select v-model="newitem.category">
              <option v-for="cat in categories" v-bind:key="cat">{{cat}}</option>
            </select></td>
          <td>
            <div class="ui checked checkbox">
              <input type="checkbox" name="hidden" v-model="newitem.hidden" />
              <label></label>
            </div>
          </td>
          <td>
            <input type="button" class="ui button" @click="save" value="Save" />
          </td>
        </tr>
      </tbody>
    </table>
    <input type="button" class="ui blue basic button" @click="crowd" value="Put julevarer i databasen" />
      </div>
      <enter-pin v-if="!pin_entered" @pincode="check_pin"/>
  </div>
</template>

<script>
import enter_pin from './manager/enter-pin.vue';
import modeswitcher from './mode-switcher.vue';
export default {
  name: "owner-mode",
  components : {
    'enter-pin' : enter_pin,
    modeswitcher
  },
  data() {
    return {
      categories : ["food","beverage","drink","goody","misc"],
        pin_entered : false,
        pincode : "",
      newitem: {
        name: "",
        price: "",
        stock: 0,
        category : "",
        hidden: false,
      },
      storeItems: [
      ],
      transactionItems: [],
    };
  },
  methods: {
    getNextId() {
      var id = 1;
      this.storeItems.forEach(function (item) {
        if (parseInt(item.id) >= id) {
          id = parseInt(item.id) + 1;
        }
      });
      return id;
    },
    check_pin(pincode) {
        if (pincode == "0000") {
            this.pin_entered = true;
        }
    },
    remove(item) {
      for (var index in this.storeItems) {
          if (this.storeItems[index].id == item.id) {
            break;
          }
      }
      this.storeItems.splice(index,1);
      this.save();
    },
    save() {
      if (this.newitem.name != "") {
          this.storeItems.push({
            id: this.getNextId(),
            name: this.newitem.name,
            price: this.newitem.price,
            category: this.newitem.category,
            stock: this.newitem.stock,
        });
        this.newitem.name = "";
        this.newitem.price = "";
      }
      this.$root.save(this.storeItems);
    },

    crowd() {
      this.storeItems = [
          {id : 1, name: "3 Æbleskiver", price: "15", category : "food", stock : 300},
          {id : 2, name: "5 Æbleskiver", price: "20", category : "food", stock : 300},
          {id : 3, name: "Gløgg", price: "5", category : "beverage", stock : ''},
          {id : 4, name: "Kaffe", price: "5", category : "beverage", stock : ''},
          {id : 5, name: "Cola 33 cl", price: "10", category : "drink", stock : 6*24},
          {id : 6, name: "Risengrød", price: "15", category : "food", stock : ''},
          {id : 7, name: "Bestik", price: "5", category : "misc", stock : ''},
          {id : 8, name: "Julegodter", price: "25", category : "goody", stock : ''},
          {id : 9, name: "Brændte mandler", price: "25", category : "goody", stock : ''},
          {id : 10, name: "Frugt", price: "5", category : "food", stock : ''},
          {id : 11, name: "Appelsin 33 cl", price: "10", category : "drink", stock : 4*24},
      ]
      this.save();
    }
  },
  mounted() {
    var self = this;
    this.$root.load(function(storeItems) {
      self.storeItems = storeItems;
    });
  },
};
</script>

<style>
</style>