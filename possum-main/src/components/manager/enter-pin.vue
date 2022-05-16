<template>
  <div class="ui segment container">
      <h1 class="ui dividing header">Enter pin</h1>
      <div class="ui action input">
        <input type="password" v-model="pincode" length="4" placeholder="Enter pin" class="ui massive" @change="$emit('pincode',pincode)" id="txtPincode">
        <a class="ui button" @click="$emit('pincode',pincode)">Enter</a>
        </div>
    </div>
</template>

<script>
export default {
  name : "enter-pin",
  data() {
    return {
      pincode : "",
    }
  },
  mounted() {
    document.getElementById("txtPincode").focus()
  }
}
</script>

<style>

</style>