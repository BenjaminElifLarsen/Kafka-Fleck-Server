<template>
<div>
  <div class="ui stackable grid">
    <div class="ui ten wide tablet column" style="background:#444; height:100vh">
        <shopitems  v-bind:buyableItems="storeItems"
                @itemclick="buy" />
    </div>
    <div class="ui two wide column tablet" style="background:#333; height:100vh" >
      <operator-actions @onbuy="completePurchase" @onreset="reset" @onremove="removeLast" />
    </div>
    <div class="ui three wide column " >
        <mode-switcher />
      <total-display v-bind:total="total" style="height:21vh"/>
      <transactionlist v-bind:list="transaction_list" style="height:70vh;overflow:scroll" />
    </div>
  </div>
</div>
</template>

<script>

import shopitems from './operator/shopitems.vue';
import totaldisplay from './operator/total-display.vue';
import transactionlist from './operator/transactionlist.vue';
import cmp_actions from './operator/actions.vue';
import mode_switcher from './mode-switcher.vue';
import axios from 'axios';
export default {
    name : "operator-mode",
    components: {shopitems, 'total-display' : totaldisplay, transactionlist, 'operator-actions' : cmp_actions, 'mode-switcher' : mode_switcher},
    data() {
        return {
            storeItems : [
                
            ],
            transactionItems : []
            }
        
    },
        methods: {
        transmitData(){
            //send data by xml
            let data = "<?xml version=\"1.0\" encoding=\"utf-8\"?> \n";
            data += "<sold:items>\n"
                for (var i in this.transactionItems) {
                data += " <sold:item> \n"
                data += "  <sold:id>" + this.transactionItems[i].id + "</sold:id>\n";
                data += "  <sold:name>" + this.transactionItems[i].name + "</sold:name>\n";
                data += "  <sold:price>" + this.transactionItems[i].price + "</sold:price>\n";
                data += " </sold:item> \n";
            }    
            data += "</sold:items>\n"
            console.log(data)
            axios.post("http://localhost:8090", {data : data});
        },
        completePurchase() {
            console.log("Bought")
            var self = this;
            this.transactionItems.forEach(function(transItem) {
                self.storeItems.forEach(function(item) {
                    if (item.id == transItem.id && item.stock > 0 && item.stock != "") {
                        item.stock--;
                    }
                });
            });

            this.transmitData();
            this.$root.save(this.storeItems);
            this.transactionItems = [];
        },
        reset() {
            console.log("Reset")
            this.transactionItems = [];

        },
        removeLast() {
            console.log("Remove last")
            this.transactionItems.pop();
        },
        buy(item) {
            
            this.transactionItems.push({
                id : item.id,
                name : item.name,
                price: item.price});
            }
        },
    computed : {
        total() {
            var sum = 0;
            this.transactionItems.forEach(function(item) {
                sum += parseFloat(item.price);
            });
            return sum;
        },
        transaction_list() {
            let map = {};
            let idx = 1;
            this.transactionItems.forEach(function(item) {
                
                if (map[item.id] == undefined) {
                    map[item.id] = {
                        index : idx++,
                        name : item.name,
                        count : 1,
                        price : parseFloat(item.price)
                    }
                } else {
                    map[item.id].count++;
                    map[item.id].price += parseFloat(item.price);
                }
            });
            return map;
        }
    },
    mounted() {
        var self = this;
        this.$root.load(function(storeItems) {
            self.storeItems = storeItems;
        });
    },
}
</script>

<style>

</style>